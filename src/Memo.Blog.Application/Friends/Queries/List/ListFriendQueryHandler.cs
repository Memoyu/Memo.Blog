using Memo.Blog.Application.Friends.Common;

namespace Memo.Blog.Application.Friends.Queries.List;

public class ListFriendQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Friend> friendRepo
    ) : IRequestHandler<ListFriendQuery, Result>
{
    public async Task<Result> Handle(ListFriendQuery request, CancellationToken cancellationToken)
    {
        var comments = await friendRepo.Select
            .Where(c=> c.Showable)
            .OrderByDescending(a => a.CreateTime)
            .ToListAsync(cancellationToken);

        var results = mapper.Map<List<ListFriendResult>>(comments);

        return Result.Success(results);
    }
}
