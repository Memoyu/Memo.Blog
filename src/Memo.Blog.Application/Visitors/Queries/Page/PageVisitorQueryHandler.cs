using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Visitors.Common;

namespace Memo.Blog.Application.Visitors.Queries.Page;

public class PageVisitorQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<PageVisitorQuery, Result>
{
    public async Task<Result> Handle(PageVisitorQuery request, CancellationToken cancellationToken)
    {
        var comments = await visitorRepo.Select
            .WhereIf(request.VisitorId.HasValue, c => c.VisitorId == request.VisitorId)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c => c.Nickname.Contains(request.Nickname!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Region), c => c.Region.Contains(request.Region!))
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, c => c.CreateTime <= request.DateEnd && c.CreateTime >= request.DateBegin)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<VisitorResult>>(comments);

        return Result.Success(new PaginationResult<VisitorResult>(results, total));
    }
}
