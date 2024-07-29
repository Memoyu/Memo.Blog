using Memo.Blog.Application.Friends.Common;

namespace Memo.Blog.Application.Friends.Queries.Page;

public class PageFriendQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Friend> friendRepo,
    IBaseDefaultRepository<FriendView> friendViewRepo
    ) : IRequestHandler<PageFriendQuery, Result>
{
    public async Task<Result> Handle(PageFriendQuery request, CancellationToken cancellationToken)
    {
        var friends = await friendRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c => c.Nickname.Contains(request.Nickname!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Description), c => c.Description.Contains(request.Description!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Site), c => c.Site.Contains(request.Site!))
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        // 获取访问记录
        var friendIds = friends.Select(a => a.FriendId).ToList();
        var views = await friendViewRepo.Select.Where(v => friendIds.Contains(v.FriendId)).ToListAsync(v => v.FriendId, cancellationToken);

        var results = mapper.Map<List<PageFriendResult>>(friends);
        foreach (var result in results)
            result.Views = views.Count(v => v == result.FriendId);

        return Result.Success(new PaginationResult<PageFriendResult>(results, total));
    }
}
