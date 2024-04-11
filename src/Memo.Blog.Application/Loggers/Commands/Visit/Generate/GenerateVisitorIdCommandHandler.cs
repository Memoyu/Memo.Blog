using Memo.Blog.Application.Common.Interfaces.Region;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Loggers.Commands.Visit.Generate;

public class GenerateVisitorIdCommandHandler(
    IMapper mapper,
    IRegionSearcher searcher,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<GenerateVisitorIdCommand, Result>
{
    public async Task<Result> Handle(GenerateVisitorIdCommand request, CancellationToken cancellationToken)
    {
        var ip = currentUserProvider.GetClientIp();
        var regionInfo = searcher.SearchInfo(ip);

        var visitor = mapper.Map<Visitor>(request);

        visitor.Country = regionInfo?.Country ?? string.Empty;
        visitor.Region = regionInfo?.Region ?? string.Empty;
        visitor.Province = regionInfo?.Province ?? string.Empty;
        visitor.City = regionInfo?.City ?? string.Empty;
        visitor.Isp = regionInfo?.Isp ?? string.Empty;

        var entity = await visitorRepo.InsertAsync(visitor, cancellationToken);
        return entity.Id <= 0 ? throw new ApplicationException("生成访客Id异常") : Result.Success(entity.VisitorId);
    }
}
