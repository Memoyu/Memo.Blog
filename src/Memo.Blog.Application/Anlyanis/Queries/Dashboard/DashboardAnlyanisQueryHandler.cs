using Memo.Blog.Application.Anlyanis.Common;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;
using static Memo.Blog.Application.Common.Extensions.DateTimeExtension;

namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;
public class DashboardAnlyanisQueryHandler(
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Moment> momentRepo,
    IBaseDefaultRepository<Friend> friendRepo,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseDefaultRepository<Visitor> visitorRepo,
    IBaseDefaultRepository<VisitStatistics> visitStatisticsRepo,
    IBaseMongoRepository<LoggerVisitCollection> loggerVisitRepo
    ) : IRequestHandler<DashboardAnlyanisQuery, Result>
{
    public async Task<Result> Handle(DashboardAnlyanisQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var weekBegin = now.Date.AddDays(-7); // 往后7天
        var weekEnd = now.Date.AddDays(-1); // 到昨天
        var weekRanges = weekBegin.GetRanges(weekEnd);

        #region 汇总数据统计

        var summaryAnlyanis = new SummaryAnlyanisResult
        {
            WeekArticles = await articleRepo.Select
                        .Where(a => a.CreateTime >= weekBegin && a.CreateTime <= now)
                        .CountAsync(cancellationToken),
            Articles = await articleRepo.Select.CountAsync(cancellationToken),
            Moments = await momentRepo.Select.CountAsync(cancellationToken),
            Friends = await friendRepo.Select.CountAsync(cancellationToken)
        };

        #endregion

        var visitorStats = await visitStatisticsRepo.Select.ToListAsync(cancellationToken);

        #region UV分析数据

        var uniqueVisitorAnlyanis = new UniqueVisitorAnlyanisResult
        {
            TodayUniqueVisitors = await visitorRepo.Select.Where(v => v.CreateTime.Date == now.Date).CountAsync(cancellationToken)
        };
        foreach (var date in weekRanges)
        {
            var total = visitorStats.Where(v => date == v.Date.Date).FirstOrDefault()?.UniqueVisitors ?? 0;
            uniqueVisitorAnlyanis.WeekUniqueVisitors.Add(new MetricItemResult(date.ToString("yyyy-MM-dd"), total));
        }

        uniqueVisitorAnlyanis.UniqueVisitors = visitorStats.Sum(a => a.UniqueVisitors) + uniqueVisitorAnlyanis.TodayUniqueVisitors;

        #endregion

        #region PV分析数据

        var pageVisitorAnlyanis = new PageVisitorAnlyanisResult();

        foreach (var date in weekRanges)
        {
            var total = visitorStats.Where(v => date == v.Date.Date).FirstOrDefault()?.PageVisitors ?? 0;
            pageVisitorAnlyanis.WeekPageVisitors.Add(new MetricItemResult(date.ToString("yyyy-MM-dd"), total));
        }

        var f = Builders<LoggerVisitCollection>.Filter.Empty;
        f &= Builders<LoggerVisitCollection>.Filter.And(
                Builders<LoggerVisitCollection>.Filter.Gte(u => u.VisitDate, now.Date),
                Builders<LoggerVisitCollection>.Filter.Lte(u => u.VisitDate, now.Date.AddDays(1).AddSeconds(-1)));
        pageVisitorAnlyanis.TodayPageVisitors = await loggerVisitRepo.CountAsync(f, cancellationToken: cancellationToken);
        pageVisitorAnlyanis.PageVisitors = visitorStats.Sum(a => a.PageVisitors) + pageVisitorAnlyanis.TodayPageVisitors;

        #endregion

        #region 评论数据统计

        var commentAnlyanis = new CommentAnlyanisResult
        {
            Comments = await commentRepo.Select.CountAsync(cancellationToken)
        };
        var weekComments = new List<int>();
        foreach (var date in weekRanges)
        {
            var count = await commentRepo.Select
                    .Where(c => c.CreateTime >= date && c.CreateTime <= date.AddDays(1).AddSeconds(-1))
                    .CountAsync(cancellationToken);
            if (date == now.Date)
                commentAnlyanis.TodayComments = count;

            commentAnlyanis.WeekComments.Add(new MetricItemResult (date.ToString("yyyy-MM-dd"), count));
        }

        #endregion

        return Result.Success(new DashboardAnlyanisResult
        {
            Summary = summaryAnlyanis,
            UniqueVisitor = uniqueVisitorAnlyanis,
            PageVisitor = pageVisitorAnlyanis,
            Comment = commentAnlyanis
        });
    }
}
