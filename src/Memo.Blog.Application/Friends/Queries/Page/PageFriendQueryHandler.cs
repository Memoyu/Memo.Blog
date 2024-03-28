using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Friends.Common;

namespace Memo.Blog.Application.Friends.Queries.Page;

public class PageFriendQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Friend> friendRepo
    ) : IRequestHandler<PageFriendQuery, Result>
{
    public async Task<Result> Handle(PageFriendQuery request, CancellationToken cancellationToken)
    {
        var comments = await friendRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c=> c.Nickname.Contains(request.Nickname!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Description), c => c.Description.Contains(request.Description!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Site), c => c.Site.Contains(request.Site!))
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<PageFriendResult>>(comments);

        return Result.Success(new PaginationResult<PageFriendResult>(results, total));
    }
}
