using Memo.Blog.Application.Common.Interfaces.Services.Region;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Loggers.Commands.Visit.Create;

public class CreateLoggerVisitCommandHadler(
     IMapper mapper,
     ILogger<CreateLoggerVisitCommandHadler> logger,
     IPublisher publisher,
     ICurrentUserProvider currentUserProvider,
     IRegionSearchService searcher,
     IBaseMongoRepository<LoggerVisitCollection> visitlogMongoRepo
    ) : IRequestHandler<CreateLoggerVisitCommand, Result>
{
    public async Task<Result> Handle(CreateLoggerVisitCommand request, CancellationToken cancellationToken)
    {
        var log = mapper.Map<LoggerVisitCollection>(request);

        log.VisitId = SnowFlakeUtil.NextId();

        var visitorId = currentUserProvider.GetCurrentVisitor();
        log.VisitorId = visitorId;

        var ip = currentUserProvider.GetClientIp();
        log.Ip = ip;
        var region = searcher.SearchInfo(ip);
        log.Country = region.Country;
        log.Region = region.Region;
        log.Province = region.Province;
        log.City = region.City;
        log.Isp = region.Isp;

        var mongoInsert = await visitlogMongoRepo.InsertOneAsync(log, null, cancellationToken);

        try
        {
            if (log.Behavior == Domain.Enums.VisitLogBehavior.ArticleDetail && log.VisitedId.HasValue)
            {
                // 更新文章浏览次数
                await publisher.Publish(new UpdatedArticleViewsEvent(log.VisitedId.Value, log.VisitorId), cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "更新文章浏览次数异常");
        }

        return mongoInsert ? Result.Success(log.VisitId) : throw new ApplicationException("保存访问日志失败");
    }
}
