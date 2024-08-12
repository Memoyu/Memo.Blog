
using Memo.Blog.Application.Configs.Common;

namespace Memo.Blog.Application.Configs.Queries.Get;

public class GetConfigQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Config> configRepo
    ) : IRequestHandler<GetConfigQuery, Result>
{
    public async Task<Result> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        var about = await configRepo.Select.FirstAsync(cancellationToken) ?? new();

        return Result.Success(mapper.Map<ConfigResult>(about));
    }
}

public class GetConfigClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Config> configRepo
    ) : IRequestHandler<GetConfigClientQuery, Result>
{
    public async Task<Result> Handle(GetConfigClientQuery request, CancellationToken cancellationToken)
    {
        var about = await configRepo.Select.FirstAsync(cancellationToken) ?? new();

        return Result.Success(mapper.Map<ConfigResult>(about));
    }
}
