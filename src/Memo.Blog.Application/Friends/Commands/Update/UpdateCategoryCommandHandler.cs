namespace Memo.Blog.Application.Friends.Commands.Update;

public class UpdateFriendCommandHandler(
    IBaseDefaultRepository<Friend> friendRepo
    ) : IRequestHandler<UpdateFriendCommand, Result>
{
    public async Task<Result> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = await friendRepo.Select.Where(c => c.FriendId == request.FriendId).FirstAsync(cancellationToken);
        if (friend == null) return Result.Failure("友链不存在");

        friend.Nickname = request.Nickname;
        friend.Description = request.Description;
        friend.Site = request.Site;
        friend.Avatar = request.Avatar ?? string.Empty;
        friend.Showable = request.Showable;
        var rows = await friendRepo.UpdateAsync(friend, cancellationToken);

        return rows > 0 ? Result.Success() : Result.Failure("更新友链失败");
    }
}
