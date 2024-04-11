using Memo.Blog.Application.Anlyanis.Common;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;
using static Memo.Blog.Application.Common.Extensions.DateTimeExtension;

namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;
public class DashboardAnlyanisQueryHandler(
    IMapper mapper,
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
        var weekBegin = now.Date.AddDays(-1); // 从昨天开始算
        var weekEnd = weekBegin.AddDays(-7); // 往后的7天
        var weekRanges = weekBegin.GetRanges(weekEnd);

        #region 汇总数据统计

        var summaryAnlyanis = new SummaryAnlyanisResult
        {
            WeekArticles = await articleRepo.Select
                        .Where(a => a.CreateTime >= weekBegin && a.CreateTime <= weekEnd)
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
            WeekUniqueVisitors = visitorStats.Where(v => weekRanges.Any(w => w == v.CreateTime.Date)).Select(v => v.UniqueVisitors).ToList(),
            TodayUniqueVisitors = await visitorRepo.Select.Where(v => v.CreateTime.Date == now.Date).CountAsync(cancellationToken)
        };

        uniqueVisitorAnlyanis.UniqueVisitors = visitorStats.Sum(a => a.UniqueVisitors) + uniqueVisitorAnlyanis.TodayUniqueVisitors;

        #endregion

        #region PV分析数据

        var pageVisitorAnlyanis = new PageVisitorAnlyanisResult
        {
            WeekPageVisitors = visitorStats.Where(v => weekRanges.Any(w => w == v.CreateTime.Date)).Select(v => v.PageVisitors).ToList(),
        };
        var f = Builders<LoggerVisitCollection>.Filter.Empty;
        f &= Builders<LoggerVisitCollection>.Filter.And(
                Builders<LoggerVisitCollection>.Filter.Gte(u => u.VisitDate, now.Date),
                Builders<LoggerVisitCollection>.Filter.Lte(u => u.VisitDate, now.Date));
        pageVisitorAnlyanis.TodayPageVisitors = await loggerVisitRepo.CountAsync(f, cancellationToken: cancellationToken);
        pageVisitorAnlyanis.PageVisitors = visitorStats.Sum(a => a.PageVisitors) + pageVisitorAnlyanis.TodayPageVisitors;

        #endregion

        #region 评论数据统计

        var commentAnlyanis = new CommentAnlyanisResult
        {
            Comments = (int)await commentRepo.Select.CountAsync(cancellationToken)
        };
        var weekComments = new List<int>();
        foreach (var day in weekRanges)
        {
            var count = (int)await commentRepo.Select
                    .Where(c => c.CreateTime >= day && c.CreateTime <= day.AddDays(1).AddSeconds(-1))
                    .CountAsync(cancellationToken);
            if (day == now.Date)
                commentAnlyanis.TodayComments = count;

            commentAnlyanis.WeekComments.Add(count);
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
