using Memo.Blog.Application.Common.Interfaces.Region;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Entities.Mongo;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Loggers.Commands.Access.Create;

public class CreateLoggerAccessCommandHadler(
     ILogger<CreateLoggerAccessCommandHadler> logger,
     IMapper mapper,
     ICurrentUserProvider currentUserProvider,
     IRegionSearcher searcher,
     IBaseMongoRepository<LoggerAccessCollection> accesslogMongoRepo
    ) : IRequestHandler<CreateLoggerAccessCommand, Result>
{
    public async Task<Result> Handle(CreateLoggerAccessCommand request, CancellationToken cancellationToken)
    {
        var log = mapper.Map<LoggerAccessCollection>(request);

        log.AccessId = SnowFlakeUtil.NextId();

        var ip = currentUserProvider.GetClientIp();
        log.Ip = ip;
        var region = searcher.Search(ip);
        log.Region = region.GetRegion();

        var mongoInsert = await accesslogMongoRepo.InsertOneAsync(log, null, cancellationToken);

        return mongoInsert ? (Result)Result.Success(log.AccessId) : throw new ApplicationException("保存访问日志失败");
    }
}
