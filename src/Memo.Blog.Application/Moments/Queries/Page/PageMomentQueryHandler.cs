using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Moments.Common;

namespace Memo.Blog.Application.Moments.Queries.Page;

public class PageMomentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<PageMomentQuery, Result>
{
    public async Task<Result> Handle(PageMomentQuery request, CancellationToken cancellationToken)
    {
        var selectMoment = momentRepo.Select;

        var tags = request.Tags ?? [];
        if (tags.Count > 0)
        {
            foreach (var tag in tags)
            {
                selectMoment.Where(m => m.Tags.Contains(tag));
            }
        }

        var moments = await selectMoment
            .WhereIf(!string.IsNullOrWhiteSpace(request.Content), m => m.Content.Contains(request.Content!))
            .WhereIf(request.TimeBegin.HasValue && request.TimeEnd.HasValue, m => m.CreateTime <= request.TimeEnd && m.CreateTime >= request.TimeBegin)
            .OrderByDescending(m => m.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<MomentResult>>(moments);

        return Result.Success(new PaginationResult<MomentResult>(results, total));
    }
}
