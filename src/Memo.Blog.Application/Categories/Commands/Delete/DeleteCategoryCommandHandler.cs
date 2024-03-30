using Memo.Blog.Domain.Events.Categories;

namespace Memo.Blog.Application.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync(cancellationToken) ?? throw new ApplicationException("分类不存在");

        category.AddDomainEvent(new DeletedCategoryEvent(category.CategoryId));

        var affrows = await categoryRepo.DeleteAsync(category, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("删除分类失败");
    }
}
