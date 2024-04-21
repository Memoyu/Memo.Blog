namespace Memo.Blog.Application.OpenSources.Commands.Delete;

[Authorize(Permissions = ApiPermission.OpenSource.Delete)]
[Transactional]
public record DeleteProjectCommand(long ProjectId) : IAuthorizeableRequest<Result>;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .Must(x => x > 0)
            .WithMessage("项目Id必须大于0");
    }
}

