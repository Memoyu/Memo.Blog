using Memo.Blog.Application.OpenSources.Common;

namespace Memo.Blog.Application.OpenSources.Queries.List;

public class ListProjectQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<OpenSource> openSourceRepo
    ) : IRequestHandler<ListProjectQuery, Result>
{
    public async Task<Result> Handle(ListProjectQuery request, CancellationToken cancellationToken)
    {
        var projects = await openSourceRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Title) || !string.IsNullOrWhiteSpace(request.Description), 
                                p => p.Title.Contains(request.Title!) || p.Description.Contains(request.Description!))
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, p => p.CreateTime <= request.DateEnd && p.CreateTime >= request.DateBegin)
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        var results = mapper.Map<List<OpenSourceResult>>(projects);

        return Result.Success(results);
    }
}

public class ListProjectClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<OpenSource> openSourceRepo
    ) : IRequestHandler<ListProjectClientQuery, Result>
{
    public async Task<Result> Handle(ListProjectClientQuery request, CancellationToken cancellationToken)
    {
        var projects = await openSourceRepo.Select
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        var results = mapper.Map<List<OpenSourceClientResult>>(projects);

        return Result.Success(results);
    }
}

