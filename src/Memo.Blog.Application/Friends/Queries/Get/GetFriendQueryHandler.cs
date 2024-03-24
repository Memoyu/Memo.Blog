using Memo.Blog.Application.Friends.Common;

namespace Memo.Blog.Application.Friends.Queries.Get;

public class GetFriendQueryHandler(IMapper mapper, IBaseDefaultRepository<Friend> friendRepo) : IRequestHandler<GetFriendQuery, Result>
{
    public async Task<Result> Handle(GetFriendQuery request, CancellationToken cancellationToken)
    {
        var friend = await friendRepo.Select.Where(f => f.FriendId == request.FriendId).FirstAsync(cancellationToken);
        if (friend is null) throw new ApplicationException("友链不存在");

        return Result.Success(mapper.Map<FriendResult>(friend));
    }
}
