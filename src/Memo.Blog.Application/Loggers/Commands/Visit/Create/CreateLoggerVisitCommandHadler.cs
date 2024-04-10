using Memo.Blog.Application.Common.Interfaces.Region;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Entities.Mongo;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Loggers.Commands.Visit.Create;

public class CreateLoggerVisitCommandHadler(
     ILogger<CreateLoggerVisitCommandHadler> logger,
     IMapper mapper,
     ICurrentUserProvider currentUserProvider,
     IRegionSearcher searcher,
     IBaseMongoRepository<LoggerVisitCollection> visitlogMongoRepo
    ) : IRequestHandler<CreateLoggerVisitCommand, Result>
{
    public async Task<Result> Handle(CreateLoggerVisitCommand request, CancellationToken cancellationToken)
    {
        var log = mapper.Map<LoggerVisitCollection>(request);

        log.VisitId = SnowFlakeUtil.NextId();

        var ip = currentUserProvider.GetClientIp();
        log.Ip = ip;
        var region = searcher.SearchInfo(ip);
        log.Country = region.Country;
        log.Region = region.Region;
        log.Province = region.Province;
        log.City = region.City;
        log.Isp = region.Isp;

        var mongoInsert = await visitlogMongoRepo.InsertOneAsync(log, null, cancellationToken);

        return mongoInsert ? Result.Success(log.VisitId) : throw new ApplicationException("保存访问日志失败");
    }
}
