namespace Memo.Blog.Application.Friends.Commands.Create;

public class CreateFriendCommandHandler(
    // ILogger<CreateFriendCommandHandler> logger,
    IMapper mapper,
    IBaseDefaultRepository<Friend> friendRepo) : IRequestHandler<CreateFriendCommand, Result>
{
    public async Task<Result> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = mapper.Map<Friend>(request);

        friend = await friendRepo.InsertAsync(friend, cancellationToken);

        return friend == null || friend.Id == 0 ? Result.Failure("保存友链失败") : Result.Success(friend.FriendId);
    }
}
