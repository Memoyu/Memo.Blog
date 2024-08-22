using Memo.Blog.Application.Configs.Common;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Configs.Queries.Get;

public class GetConfigQueryHandler(
    IMapper mapper,
    IConfigRepository configRepo
    ) : IRequestHandler<GetConfigQuery, Result>
{
    public async Task<Result> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        var config = await configRepo.GetWithInitAsync(cancellationToken);

        return Result.Success(mapper.Map<ConfigResult>(config));
    }
}

public class GetConfigVisitorQueryHandler(
    ICurrentUserProvider currentUserProvider,
    IConfigRepository configRepo
    ) : IRequestHandler<GetConfigVisitorQuery, Result>
{
    public async Task<Result> Handle(GetConfigVisitorQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserProvider.GetCurrentUser().Id;

        var config = await configRepo.GetWithInitAsync(cancellationToken);
        var userVisitors = config.Visitors.ToDesJson<List<VisitorConfigResult>>() ?? [];
        var visitor = userVisitors.FirstOrDefault(v => v.UserId == userId) ?? new();

        return Result.Success(new VisitorConfigResult { UserId = userId, VisitorId = visitor.VisitorId, Avatar = visitor.Avatar, Nickname = visitor.Nickname });
    }
}

public class GetConfigClientQueryHandler(
    IMapper mapper,
    IConfigRepository configRepo
    ) : IRequestHandler<GetConfigClientQuery, Result>
{
    public async Task<Result> Handle(GetConfigClientQuery request, CancellationToken cancellationToken)
    {
        var config = await configRepo.GetWithInitAsync(cancellationToken);

        return Result.Success(mapper.Map<ConfigClientResult>(config));
    }
}
