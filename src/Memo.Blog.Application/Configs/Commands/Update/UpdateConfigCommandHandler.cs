using Memo.Blog.Application.Configs.Common;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Configs.Commands.Update;

public class UpdateConfigCommandHandler(
    IMapper mapper,
    IConfigRepository configRepo
    ) : IRequestHandler<UpdateConfigCommand, Result>
{
    public async Task<Result> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        var update = mapper.Map<Config>(request);
        var config = await configRepo.GetWithInitAsync(cancellationToken);
        update.Id = config.Id;
        update.Visitors = config.Visitors; // 不更新回复访客配置
        var affrows = await configRepo.UpdateAsync(update, cancellationToken);
        return affrows > 0 ? Result.Success(update.Id) : Result.Failure("更新系统配置失败");
    }
}

public class UpdateConfigBannerCommandHandler(
    IConfigRepository configRepo
    ) : IRequestHandler<UpdateConfigBannerCommand, Result>
{
    public async Task<Result> Handle(UpdateConfigBannerCommand request, CancellationToken cancellationToken)
    {
        var config = await configRepo.GetWithInitAsync(cancellationToken);
        config.Banner = request.Banner.ToJson();
        var affrows = await configRepo.UpdateAsync(config, cancellationToken);
        return affrows > 0 ? Result.Success(config.Id) : Result.Failure("更新系统配置失败");
    }
}

public class UpdateVisitorConfigCommandHandler(
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Visitor> visitorRepo,
    IConfigRepository configRepo
    ) : IRequestHandler<UpdateVisitorConfigCommand, Result>
{
    public async Task<Result> Handle(UpdateVisitorConfigCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserProvider.GetCurrentUser().Id;
        if (userId == 0) throw new ApplicationException("获取当前用户信息失败");

        var visitor = await visitorRepo.Select.Where(v => v.VisitorId == request.VisitorId).FirstAsync(cancellationToken)
            ?? throw new ApplicationException("访客信息不存在");

        var config = await configRepo.GetWithInitAsync(cancellationToken);

        var userVisitors = config.Visitors.ToDesJson<List<VisitorConfigResult>>() ?? [];
        var userVisitor = userVisitors.FirstOrDefault(v => v.UserId == userId);
        if (userVisitor == null)
        {
            userVisitor = new VisitorConfigResult { UserId = userId, VisitorId = visitor.VisitorId, Avatar = visitor.Avatar, Nickname = visitor.Nickname };
            userVisitors.Add(userVisitor);
        }
        else
        {
            userVisitor.VisitorId = visitor.VisitorId;
            userVisitor.Avatar = visitor.Avatar;
            userVisitor.Nickname = visitor.Nickname;
        }

        config.Visitors = userVisitors.ToJson();
        var affrows = await configRepo.UpdateAsync(config, cancellationToken);
        return affrows > 0 ? Result.Success(userVisitor) : Result.Failure("更新管理员回复访客配置失败");
    }
}
