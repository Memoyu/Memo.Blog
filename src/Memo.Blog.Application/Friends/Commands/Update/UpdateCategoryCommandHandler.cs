namespace Memo.Blog.Application.Friends.Commands.Update;

public class UpdateFriendCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Friend> friendRepo
    ) : IRequestHandler<UpdateFriendCommand, Result>
{
    public async Task<Result> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = await friendRepo.Select.Where(c => c.FriendId == request.FriendId).FirstAsync(cancellationToken) ?? throw new ApplicationException("友链不存在");
       
        var update = mapper.Map<Friend>(request);
        update.Id = friend.Id;
        update.Views = friend.Views;
        var affrows = await friendRepo.UpdateAsync(update, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新友链失败");
    }
}
