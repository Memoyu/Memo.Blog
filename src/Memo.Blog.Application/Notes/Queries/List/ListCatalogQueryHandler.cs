using Amazon.Runtime.Internal.Transform;
using Memo.Blog.Application.Notes.Common;

namespace Memo.Blog.Application.Notes.Queries.List;

public class ListCatalogQueryHandler(
      IBaseDefaultRepository<Note> noteRepo,
      IBaseDefaultRepository<NoteCatalog> noteCatalogRepo
    ) : IRequestHandler<ListCatalogQuery, Result>
{
    public async Task<Result> Handle(ListCatalogQuery request, CancellationToken cancellationToken)
    {
        // 获取全部目录，笔记
        var catalogs = await noteCatalogRepo.Select.ToListAsync(cancellationToken);
        var notes = await noteRepo.Select.ToListAsync(n => new { n.NoteId, n.CatalogId, n.Title }, cancellationToken);

        var groupCatalogs = catalogs.GroupBy(c => c.ParentId).ToList();
        var groupNotes = notes.GroupBy(c => c.CatalogId).ToList();

        var dict = new Dictionary<string, List<ListCatalogResultItem>>
        {
            // 初始化根目录
            {
                string.Empty,
                catalogs.Where(c => !c.ParentId.HasValue).Select(c => new ListCatalogResultItem
                {
                    Id = c.CatalogId,
                    Type = 0,
                    Title = c.Title,
                    Count = GetCatalogCount(c.CatalogId)
                }).ToList()
            }
        };

        foreach (var catalog in catalogs)
        {
            var key = catalog.CatalogId.ToString();
            var has = dict.TryGetValue(key, out var list);
            if (!has)
            {
                list = [];
                dict.Add(key, list);
            }

            var childs = groupCatalogs.FirstOrDefault(g => g.Key == catalog.CatalogId)?.ToList() ?? [];
            var childNotes = groupNotes.FirstOrDefault(g => g.Key == catalog.CatalogId)?.ToList() ?? [];

            // 插入目录
            list!.AddRange(childs.Select(c => new ListCatalogResultItem
            {
                Id = c.CatalogId,
                Type = 0,
                Title = c.Title,
                Count = GetCatalogCount(c.CatalogId)
            }));

            // 再插入笔记
            list!.AddRange(childNotes.Select(n => new ListCatalogResultItem
            {
                Id = n.NoteId,
                Type = 1,
                Title = n.Title,
            }));
        }

        return Result.Success(dict);

        int GetCatalogCount(long catalogId)
        {
            var catalogCount = groupCatalogs.FirstOrDefault(g => g.Key == catalogId)?.Count() ?? 0;
            var noteCount = groupNotes.FirstOrDefault(g => g.Key == catalogId)?.Count() ?? 0;
            return catalogCount + noteCount;
        }
    }
}
