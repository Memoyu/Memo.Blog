namespace Memo.Blog.Application.Friends.Commands.Delete;

[Authorize(Permissions = ApiPermission.Friend.Delete)]
public record DeleteFriendCommand(long FriendId) : IAuthorizeableRequest<Result>;

public class DeleteFriendCommandValidator : AbstractValidator<DeleteFriendCommand>
{
    public DeleteFriendCommandValidator()
    {
        RuleFor(x => x.FriendId)
            .Must(x => x > 0)
            .WithMessage("友链Id必须大于0");
    }
}

