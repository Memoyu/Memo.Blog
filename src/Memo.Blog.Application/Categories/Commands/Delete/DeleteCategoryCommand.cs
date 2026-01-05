namespace Memo.Blog.Application.Categories.Commands.Delete;

[Authorize(Permissions = ApiPermission.Category.Delete)]
[Transactional]
public record DeleteCategoryCommand(long CategoryId) : IAuthorizeableRequest<Result>;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("分类Id必须大于0");

        RuleFor(x => x.CategoryId)
            .Equal(InitConst.InitCategoryId)
            .WithMessage("默认分类，无法删除");

        RuleFor(x => x.CategoryId)
            .Must(x => x != InitConst.InitCategoryId)
            .WithMessage("初始分类无法删除");
    }
}

