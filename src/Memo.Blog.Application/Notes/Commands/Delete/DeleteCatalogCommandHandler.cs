using Memo.Blog.Domain.Events.Categories;

namespace Memo.Blog.Application.Notes.Commands.Delete;

public class DeleteCatalogCommandHandler(
    IBaseDefaultRepository<NoteCatalog> noteCatalogRepo
    ) : IRequestHandler<DeleteCatalogCommand, Result>
{
    public async Task<Result> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = await noteCatalogRepo.Select.Where(c => c.CatalogId == request.CatalogId).FirstAsync(cancellationToken) ?? 
            throw new ApplicationException("目录不存在或已删除");

        catalog.AddDomainEvent(new DeletedCategoryEvent(catalog.CatalogId));
        // 删除笔记目录
        var row = await noteCatalogRepo.DeleteAsync(catalog, cancellationToken);
        return row <= 0 ? throw new ApplicationException("删除目录失败") : (Result)Result.Success(catalog.CatalogId);
    }
}
