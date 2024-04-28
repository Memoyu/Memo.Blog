using Memo.Blog.Application.Common.Interfaces.Region;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Visitors.Commands.Generate;

public class CreateVisitorCommandHandler(
    IMapper mapper,
    IRegionSearcher searcher,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<CreateVisitorCommand, Result>
{
    public async Task<Result> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
    {
        var visitor = mapper.Map<Visitor>(request);

        var ip = currentUserProvider.GetClientIp();
        var region = searcher.Search(ip);
        visitor.Ip = ip;
        visitor.Region = region ?? string.Empty;

        var entity = await visitorRepo.InsertAsync(visitor, cancellationToken);
        return entity.Id <= 0 ? throw new ApplicationException("生成访客Id异常") : Result.Success(entity.VisitorId);
    }
}
