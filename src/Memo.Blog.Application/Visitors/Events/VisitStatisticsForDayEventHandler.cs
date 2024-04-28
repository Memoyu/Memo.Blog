using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Visitors;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using static Memo.Blog.Application.Common.Extensions.DateTimeExtension;

namespace Memo.Blog.Application.Visitors.Events;

public class VisitStatisticsForDayEventHandler(
    ILogger<VisitStatisticsForDayEventHandler> logger,
    IBaseDefaultRepository<Visitor> visitorRepo,
    IBaseMongoRepository<LoggerVisitCollection> visitLogRepo,
    IBaseDefaultRepository<VisitStatistics> visitStatisticsRepo
    ) : INotificationHandler<VisitStatisticsForDayEvent>
{
    public async Task Handle(VisitStatisticsForDayEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var now = DateTime.Now.Date;
            var weekBegin = now.Date.AddDays(-7); // 往后7天
            var weekEnd = now.Date.AddDays(-1); // 到昨天
            var weeks = weekBegin.GetRanges(weekEnd) ?? [];
            var stats = await visitStatisticsRepo.Select
                .Where(vs => weeks.Contains(vs.Date.Date))
                .ToListAsync(cancellationToken);

            foreach (var day in weeks)
            {
                var stat = stats.FirstOrDefault(s => s.Date.Date == day.Date);

                // 已存在统计数据，则不要再重新统计
                if (stat != null) continue;

                // 获取当前UV数据
                var dayUv = await visitorRepo.Select.Where(v => day.Date == v.CreateTime.Date).CountAsync(cancellationToken);

                // 获取当天PV数据
                var f = Builders<LoggerVisitCollection>.Filter.Empty;
                f &= Builders<LoggerVisitCollection>.Filter.And(
                        Builders<LoggerVisitCollection>.Filter.Gte(u => u.VisitDate, day.Date),
                        Builders<LoggerVisitCollection>.Filter.Lte(u => u.VisitDate, day.Date.AddDays(1).AddSeconds(-1)));
                var dayPv = await visitLogRepo.CountAsync(f, cancellationToken: cancellationToken);

                var entity = await visitStatisticsRepo.InsertAsync(
                    new VisitStatistics
                    {
                        Date = day.Date,
                        UniqueVisitors = dayUv,
                        PageVisitors = dayPv,
                    }, cancellationToken);

                if (entity.Id <= 0)
                    logger.LogError("写入访问汇总统计数据错误");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "写入访问汇总统计数据异常");
        }
    }
}
