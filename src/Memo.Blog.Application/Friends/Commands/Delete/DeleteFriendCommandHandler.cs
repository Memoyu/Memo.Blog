namespace Memo.Blog.Application.Friends.Commands.Delete;

public class DeleteFriendCommandHandler(
    IBaseDefaultRepository<Friend> friendRepo
    ) : IRequestHandler<DeleteFriendCommand, Result>
{
    public async Task<Result> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
    {
        var category = await friendRepo.Select.Where(c => c.FriendId == request.FriendId).FirstAsync(cancellationToken) ?? throw new ApplicationException("友链不存在");
        var affrows = await friendRepo.DeleteAsync(category, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("删除友链失败");
    }
}
