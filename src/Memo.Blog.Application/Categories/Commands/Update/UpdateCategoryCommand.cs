namespace Memo.Blog.Application.Categories.Commands.Update;

[Authorize(Permissions = ApiPermission.Category.Update)]
[Transactional]
public record UpdateCategoryCommand(long CategoryId, string Name) : IRequest<Result>;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .Must(x => x > 0)
            .WithMessage("分类Id必须大于0");

        RuleFor(x => x.Name)
           .MinimumLength(1)
           .MaximumLength(10)
           .WithMessage("分类名称长度在1-10个字符之间");
    }
}

