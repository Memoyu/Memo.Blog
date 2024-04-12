using Memo.Blog.Application.Anlyanis.Common;

namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;

public class UvMapAnlyanisQueryHandler(IBaseDefaultRepository<Visitor> visitorRepo) : IRequestHandler<UvMapAnlyanisQuery, Result>
{
    public async Task<Result> Handle(UvMapAnlyanisQuery request, CancellationToken cancellationToken)
    {
        var visitors = await visitorRepo.Select.ToListAsync(cancellationToken);

        var visitorGroups = visitors.GroupBy(x => x.City).ToList();

        var result = visitorGroups.Select(g => new MetricItemResult(g.Key, g.Count())).ToList();

        return Result.Success(result);
    }
}
