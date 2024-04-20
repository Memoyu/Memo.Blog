namespace Memo.Blog.Application.OpenSources.Commands.Create;

[Authorize(Permissions = ApiPermission.OpenSource.Create)]
public record CreateProjectCommand(
    long? RepoId,
    string Title,
    string Description,
    string? ImageUrl,
    string? ReadmeUrl
    ) : IAuthorizeableRequest<Result>;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty()
           .WithMessage("项目名称不能为空");

        RuleFor(x => x.Description)
            .MinimumLength(1)
            .MaximumLength(100)
            .WithMessage("项目描述长度在1-100个字符之间");
    }
}
