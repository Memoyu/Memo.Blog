namespace Memo.Blog.Application.OpenSources.Commands.Update;

[Authorize(Permissions = ApiPermission.OpenSource.Update)]
[Transactional]
public record UpdateProjectCommand(
    long ProjectId,
    long? RepoId,
    string Title,
    string Description,
    string? ImageUrl,
    string? ReadmeUrl) : IAuthorizeableRequest<Result>;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .Must(x => x > 0)
            .WithMessage("项目Id必须大于0");

        RuleFor(x => x.Title)
           .NotEmpty()
           .WithMessage("项目名称不能为空");

        RuleFor(x => x.Description)
            .MinimumLength(1)
            .MaximumLength(100)
            .WithMessage("项目描述长度在1-100个字符之间");
    }
}

