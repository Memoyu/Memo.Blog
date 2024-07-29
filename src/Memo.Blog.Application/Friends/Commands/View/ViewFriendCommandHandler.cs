using EasyCaching.Core;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Friends.Commands.View;

public class ViewFriendCommandHandler(
    ICurrentUserProvider currentUserProvider,
    IEasyCachingProvider ecProvider,
    IBaseDefaultRepository<Friend> friendRepo,
    IBaseDefaultRepository<FriendView> friendViewRepo
    ) : IRequestHandler<ViewFriendCommand, Result>
{
    public async Task<Result> Handle(ViewFriendCommand request, CancellationToken cancellationToken)
    {
        var visitorId = currentUserProvider.GetCurrentVisitor();
        if (visitorId == 0) throw new ApplicationException("当前访客为空");

        // 做访问频率限制，在时间段多次访问，仅记录一次
        var cacheKey = CacheKeyConst.FriendView(request.FriendId, visitorId);
        var view = await ecProvider.GetAsync<long>(cacheKey, cancellationToken);
        if (view != null && view.HasValue) return Result.Success(view.Value);

        var exist = await friendRepo.Select.AnyAsync(c => c.FriendId == request.FriendId, cancellationToken);
        if (!exist) throw new ApplicationException("友链不存在");

        var friendView = await friendViewRepo.InsertAsync(new FriendView { FriendId = request.FriendId, VisitorId = visitorId }, cancellationToken);
        if (friendView == null || friendView.Id == 0) return Result.Failure("保存友链访问记录失败");

        await ecProvider.SetAsync(cacheKey, friendView.Id, TimeSpan.FromMinutes(10), cancellationToken); // 十分钟内同一个访客，只算一次浏览次数

        return Result.Success(friendView.Id);
    }
}
