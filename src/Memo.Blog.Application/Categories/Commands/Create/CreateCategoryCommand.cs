namespace Memo.Blog.Application.Categories.Commands.Create;

[Authorize(Permissions = ApiPermission.Category.Create)]
public record CreateCategoryCommand(
    string Name
    ) : IRequest<Result>;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("分类名称不能为空");

        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(10)
            .WithMessage("分类名称长度在1-10个字符之间");
    }
}
