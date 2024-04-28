using Memo.Blog.Application.Anlyanis.Common;
using Memo.Blog.Application.Visitors.Common;

namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;

public class UvMapAnlyanisQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Visitor> visitorRepo) : IRequestHandler<UvMapAnlyanisQuery, Result>
{
    public async Task<Result> Handle(UvMapAnlyanisQuery request, CancellationToken cancellationToken)
    {
        var visitors = await visitorRepo.Select.ToListAsync(cancellationToken);
        var results = mapper.Map<List<VisitorWithDetailRegionResult>>(visitors);

        var visitorGroups = results.GroupBy(x => x.City).ToList();

        var result = visitorGroups.Select(g => new MetricItemResult(g.Key, g.Count())).ToList();

        return Result.Success(result);
    }
}
