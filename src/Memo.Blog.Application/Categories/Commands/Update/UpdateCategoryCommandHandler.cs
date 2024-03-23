namespace Memo.Blog.Application.Categories.Commands.Update;

public class UpdateCategoryCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync(cancellationToken);
        if (category == null) return Result.Failure("分类不存在");

        var exist = await categoryRepo.Select.AnyAsync(c => c.CategoryId != request.CategoryId && request.Name == c.Name, cancellationToken);
        if (exist) return Result.Failure("分类名已存在");

        category.Name = request.Name;
        var rows = await categoryRepo.UpdateAsync(category, cancellationToken);

        return rows > 0 ? Result.Success() : Result.Failure("更新分类失败");
    }
}
