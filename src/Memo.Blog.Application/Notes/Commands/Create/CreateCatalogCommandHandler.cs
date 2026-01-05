namespace Memo.Blog.Application.Notes.Commands.Create;

public class CreateCatalogCommandHandler(
    IBaseDefaultRepository<NoteCatalog> noteCatalogRepo
    ) : IRequestHandler<CreateCatalogCommand, Result>
{
    public async Task<Result> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue && !await noteCatalogRepo.Select.AnyAsync(c => c.CatalogId == request.ParentId, cancellationToken))
            throw new ApplicationException("父目录不存在或已删除");

        if (await noteCatalogRepo.Select.AnyAsync(c => c.Title == request.Title, cancellationToken))
            throw new ApplicationException("同名目录已存在");

        var catalog = new NoteCatalog
        {
            ParentId = request.ParentId,
            Title = request.Title,
        };
        catalog = await noteCatalogRepo.InsertAsync(catalog, cancellationToken);

        return catalog == null || catalog.Id == 0 ? throw new ApplicationException("保存目录失败") : Result.Success(catalog.CatalogId);
    }
}
