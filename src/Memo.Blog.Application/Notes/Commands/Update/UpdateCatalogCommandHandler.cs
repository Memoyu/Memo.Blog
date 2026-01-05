
namespace Memo.Blog.Application.Notes.Commands.Update;

public class UpdateCatalogCommandHandler(IBaseDefaultRepository<NoteCatalog> noteCatalogRepo) : IRequestHandler<UpdateCatalogCommand, Result>
{
    public async Task<Result> Handle(UpdateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = await noteCatalogRepo.Select.Where(c => c.CatalogId == request.CatalogId).FirstAsync(cancellationToken) ??
            throw new ApplicationException("目录不存在或已删除");

        catalog.Title = request.Title;
        var affrows = await noteCatalogRepo.UpdateAsync(catalog, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新目录失败");
    }
}
