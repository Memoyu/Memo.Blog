namespace Memo.Blog.Application.Visitors.Commands.Delete;

[Authorize(Permissions = ApiPermission.Visitor.Delete)]
public record DeleteVisitorCommand(long VisitorId) : IAuthorizeableRequest<Result>;

public class DeleteVisitorCommandValidator : AbstractValidator<DeleteVisitorCommand>
{
    public DeleteVisitorCommandValidator()
    {
        RuleFor(x => x.VisitorId)
            .Must(x => x > 0)
            .WithMessage("访客Id必须大于0");
    }
}

