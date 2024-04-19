namespace Memo.Blog.Application.OpenSources.Commands.Create;

[Authorize(Permissions = ApiPermission.OpenSource.Create)]
public record CreateProjectCommand(
    string Name
    ) : IAuthorizeableRequest<Result>;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
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
