namespace Memo.Blog.Application.Friends.Commands.View;

public record ViewFriendCommand(long FriendId) : IAuthorizeableRequest<Result>;

public class ViewFriendCommandValidator : AbstractValidator<ViewFriendCommand>
{
    public ViewFriendCommandValidator()
    {
        RuleFor(x => x.FriendId)
            .Must(x => x > 0)
            .WithMessage("友链Id必须大于0");
    }
}

