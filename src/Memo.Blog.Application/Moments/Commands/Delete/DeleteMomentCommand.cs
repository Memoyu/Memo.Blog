namespace Memo.Blog.Application.Moments.Commands.Delete;

[Authorize(Permissions = ApiPermission.Moment.Delete)]
public record DeleteMomentCommand(
    long MomentId
    ) : IAuthorizeableRequest<Result>;

public class DeleteMomentCommandValidator : AbstractValidator<DeleteMomentCommand>
{
    public DeleteMomentCommandValidator()
    {
        RuleFor(x => x.MomentId)
            .Must(x => x > 0)
            .WithMessage("动态Id必须大于0");
    }
}
