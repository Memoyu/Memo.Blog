using Memo.Blog.Application.Common.Interfaces.Services.Region;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Visitors.Commands.Update;

public class UpdateVisitorCommandHandler(
    IMapper mapper,
    IRegionSearchService searcher,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<UpdateVisitorCommand, Result>
{
    public async Task<Result> Handle(UpdateVisitorCommand request, CancellationToken cancellationToken)
    {
        // 不传访客id, 就从请求头拿
        long? visitorId = null;
        if (!request.VisitorId.HasValue)
            visitorId = currentUserProvider.GetCurrentVisitor();

        // 有访客id, 就去获取数据
        Visitor? entity = null;
        if (visitorId.HasValue)
            entity = await visitorRepo.Select.Where(c => c.VisitorId == visitorId.Value).FirstAsync(cancellationToken);
        
        var affrows = 0;
        var visitor = mapper.Map<Visitor>(request);
        var ip = currentUserProvider.GetClientIp();
        var region = searcher.Search(ip);
        visitor.Ip = ip;
        visitor.Region = region ?? string.Empty;

        // 不存在时，则新建一个访客
        if (entity == null)
        {
            visitor = await visitorRepo.InsertAsync(visitor, cancellationToken);
            if (visitor.Id > 0) affrows = 1;
        }
        else
        {
            visitor.Id = entity.Id;
            visitor.VisitorId = entity.VisitorId;
            affrows = await visitorRepo.UpdateAsync(visitor, cancellationToken);
        }

        return affrows > 0 ? Result.Success(visitor.VisitorId) : throw new ApplicationException("更新访客失败");
    }
}
