namespace Memo.Blog.Application.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var tag = await categoryRepo.Select.Where(t => t.CategoryId == request.CategoryId).ToOneAsync(cancellationToken);
        if (tag == null) return Result.Failure("分类不存在");

        var rows = await categoryRepo.DeleteAsync(tag, cancellationToken);

        return rows > 0 ? Result.Success() : Result.Failure("删除分类失败");
    }
}
