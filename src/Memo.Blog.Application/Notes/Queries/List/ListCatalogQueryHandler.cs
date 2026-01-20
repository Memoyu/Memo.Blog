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
        var notes = await noteRepo.Select.ToListAsync(n => new SimpleNoteDto { NoteId = n.NoteId, GroupId = n.GroupId, Title = n.Title }, cancellationToken);

        var groupGroups = groups.GroupBy(c => c.ParentId).ToList();
        var groupNotes = notes.GroupBy(c => c.GroupId).ToList();

        var dict = new Dictionary<string, List<ListGroupResultItem>>
        {
            // 初始化根分组
            {
                string.Empty,
                GroupsToItems( groups.Where(c => !c.ParentId.HasValue))// 分组
                .Concat(request.OnlyGroup == true ? [] : NotesToItems(notes.Where(n => !n.GroupId.HasValue)))// 笔记
                .ToList()
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
            list!.AddRange(GroupsToItems(childs));

            // 只加载分组
            if (request.OnlyGroup == true) continue;

            // 再插入笔记
            list!.AddRange(NotesToItems(childNotes));
        }

        return Result.Success(dict);

        List<ListGroupResultItem> GroupsToItems(IEnumerable<NoteGroup> groups)
        {
            return groups.Select(c => new ListGroupResultItem
            {
                Id = c.GroupId,
                Type = 0,
                Title = c.Title,
                Count = GetGroupCount(c.GroupId)
            }).ToList();
        }

        List<ListGroupResultItem> NotesToItems(IEnumerable<SimpleNoteDto> notes)
        {
            return notes.Select(n => new ListGroupResultItem
            {
                Id = n.NoteId,
                Type = 1,
                Title = n.Title,
            }).ToList();
        }

        int GetGroupCount(long groupId)
        {
            var groupCount = groupGroups.FirstOrDefault(g => g.Key == groupId)?.Count() ?? 0;
            var noteCount = 0;
            if (request.OnlyGroup != true)
                noteCount = groupNotes.FirstOrDefault(g => g.Key == groupId)?.Count() ?? 0;
            
            return groupCount + noteCount;
        }
    }
}
