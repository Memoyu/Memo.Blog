namespace Memo.Blog.Application.Categories.Commands.Delete;

[Authorize(Permissions = ApiPermission.Category.Delete)]
[Transactional]
public record DeleteCategoryCommand(long CategoryId) : IAuthorizeableRequest<Result>;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .Must(x => x > 0)
            .WithMessage("分类Id必须大于0");

        RuleFor(x => x.CategoryId)
            .Must(x => x != InitConst.InitCategoryId)
            .WithMessage("初始分类无法删除");
    }
}

