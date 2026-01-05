using Memo.Blog.Domain.Events.Notes;

namespace Memo.Blog.Application.Notes.Events;

public class DeletedNoteCatalogEventHandler(
    IBaseDefaultRepository<NoteCatalog> noteCatalogRepo,
    IBaseDefaultRepository<Note> noteRepo
    ) : INotificationHandler<DeletedNoteCatalogEvent>
{
    public async Task Handle(DeletedNoteCatalogEvent notification, CancellationToken cancellationToken)
    {
        // 处理未存在初始化默认目录的情况
        var initCatalog = await noteCatalogRepo.Select.Where(c => c.Title == InitConst.InitNoteCatalogTitle).FirstAsync(cancellationToken);
        if (initCatalog == null)
        {
            initCatalog = await noteCatalogRepo.InsertAsync(new NoteCatalog { CatalogId = InitConst.InitNoteCatalogId, Title = InitConst.InitNoteCatalogTitle }, cancellationToken);
        }
        else if (initCatalog.CatalogId != InitConst.InitNoteCatalogId)
        {
            initCatalog.CatalogId = InitConst.InitCategoryId;
            await noteCatalogRepo.UpdateAsync(initCatalog, cancellationToken);
        }

        // 获取目录相关笔记，更新为默认目录
        var notes = await noteRepo.Select.Where(a => a.CatalogId == notification.catalogId).ToListAsync(cancellationToken) ?? new();
        notes.ForEach(n => n.CatalogId = initCatalog.CatalogId);
        var affrows = await noteRepo.UpdateAsync(notes, cancellationToken);
    }
}
