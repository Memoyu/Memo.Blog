namespace Memo.Blog.Application.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).ToOneAsync(cancellationToken);
        if (category == null) return Result.Failure("分类不存在");

        var rows = await categoryRepo.DeleteAsync(category, cancellationToken);

        return rows > 0 ? Result.Success() : Result.Failure("删除分类失败");
    }
}
