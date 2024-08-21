using Memo.Blog.Application.Configs.Common;

namespace Memo.Blog.Application.Configs.Queries.Get;

public class GetConfigQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Config> configRepo
    ) : IRequestHandler<GetConfigQuery, Result>
{
    public async Task<Result> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        var config = await configRepo.Select.FirstAsync(cancellationToken);

        return Result.Success(mapper.Map<ConfigResult>(config));
    }
}

public class GetConfigAdminQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Config> configRepo
    ) : IRequestHandler<GetConfigAdminQuery, Result>
{
    public async Task<Result> Handle(GetConfigAdminQuery request, CancellationToken cancellationToken)
    {
        var config = await configRepo.Select.FirstAsync(cancellationToken) ?? new();

        return Result.Success(mapper.Map<ConfigAdminResult>(config));
    }
}

public class GetConfigClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Config> configRepo
    ) : IRequestHandler<GetConfigClientQuery, Result>
{
    public async Task<Result> Handle(GetConfigClientQuery request, CancellationToken cancellationToken)
    {
        var config = await configRepo.Select.FirstAsync(cancellationToken) ?? new();

        return Result.Success(mapper.Map<ConfigClientResult>(config));
    }
}
