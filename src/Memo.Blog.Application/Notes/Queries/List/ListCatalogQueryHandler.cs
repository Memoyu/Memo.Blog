using Memo.Blog.Application.Notes.Common;

namespace Memo.Blog.Application.Notes.Queries.List;

public class ListCatalogQueryHandler(
      IBaseDefaultRepository<Note> noteRepo,
      IBaseDefaultRepository<NoteGroup> noteGroupRepo
    ) : IRequestHandler<ListCatalogQuery, Result>
{
    public async Task<Result> Handle(ListCatalogQuery request, CancellationToken cancellationToken)
    {
        // 获取全部分组，笔记
        var groups = await noteGroupRepo.Select.ToListAsync(cancellationToken);
        var notes = await noteRepo.Select.ToListAsync(n => new { n.NoteId, n.GroupId, n.Title }, cancellationToken);

        var groupGroups = groups.GroupBy(c => c.ParentId).ToList();
        var groupNotes = notes.GroupBy(c => c.GroupId).ToList();

        var dict = new Dictionary<string, List<ListGroupResultItem>>
        {
            // 初始化根分组
            {
                string.Empty,
                groups.Where(c => !c.ParentId.HasValue).Select(c => new ListGroupResultItem
                {
                    Id = c.GroupId,
                    Type = 0,
                    Title = c.Title,
                    Count = GetGroupCount(c.GroupId)
                }).ToList()
            }
        };

        foreach (var group in groups)
        {
            var key = group.GroupId.ToString();
            var has = dict.TryGetValue(key, out var list);
            if (!has)
            {
                list = [];
                dict.Add(key, list);
            }

            var childs = groupGroups.FirstOrDefault(g => g.Key == group.GroupId)?.ToList() ?? [];
            var childNotes = groupNotes.FirstOrDefault(g => g.Key == group.GroupId)?.ToList() ?? [];

            // 插入分组
            list!.AddRange(childs.Select(c => new ListGroupResultItem
            {
                Id = c.GroupId,
                Type = 0,
                Title = c.Title,
                Count = GetGroupCount(c.GroupId)
            }));

            // 再插入笔记
            list!.AddRange(childNotes.Select(n => new ListGroupResultItem
            {
                Id = n.NoteId,
                Type = 1,
                Title = n.Title,
            }));
        }

        return Result.Success(dict);

        int GetGroupCount(long groupId)
        {
            var groupCount = groupGroups.FirstOrDefault(g => g.Key == groupId)?.Count() ?? 0;
            var noteCount = groupNotes.FirstOrDefault(g => g.Key == groupId)?.Count() ?? 0;
            return groupCount + noteCount;
        }
    }
}
