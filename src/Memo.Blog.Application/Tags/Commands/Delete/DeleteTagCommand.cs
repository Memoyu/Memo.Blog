namespace Memo.Blog.Application.Tags.Commands.Delete;

[Authorize(Permissions = ApiPermission.Tag.Delete)]
[Transactional]
public record DeleteTagCommand(long TagId) : IRequest<Result>;

public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(x => x.TagId)
            .Must(x => x > 0)
            .WithMessage("标签Id必须大于0");
    }
}

