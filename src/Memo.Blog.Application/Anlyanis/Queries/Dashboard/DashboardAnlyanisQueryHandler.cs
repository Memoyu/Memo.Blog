
using Memo.Blog.Application.Anlyanis.Common;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Domain.Entities.Mongo;
using static Memo.Blog.Application.Common.Extensions.DateTimeExtension;

namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;
public class DashboardAnlyanisQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Moment> momentRepo,
    IBaseDefaultRepository<Friend> friendRepo,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseMongoRepository<LoggerVisitCollection> logVisitRepo
    ) : IRequestHandler<DashboardAnlyanisQuery, Result>
{
    public async Task<Result> Handle(DashboardAnlyanisQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var (weekBegin, weekEnd) = now.GetRange(DataTimeRangeType.Week);
        var weekRanges = now.GetRanges(DataTimeRangeType.Week);

        #region 汇总数据统计

        var summaryAnlyanis = new SummaryAnlyanisResult
        {
            WeekArticles = (int)await articleRepo.Select
                        .Where(a => a.CreateTime >= weekBegin && a.CreateTime <= weekEnd)
                        .CountAsync(cancellationToken),
            Articles = (int)await articleRepo.Select.CountAsync(cancellationToken),
            Moments = (int)await momentRepo.Select.CountAsync(cancellationToken),
            Friends = (int)await friendRepo.Select.CountAsync(cancellationToken)
        };

        #endregion

        #region UV分析数据

        var uniqueVisitorAnlyanis = new UniqueVisitorAnlyanisResult();

        #endregion

        #region PV分析数据

        var pageVisitorAnlyanis = new PageVisitorAnlyanisResult();

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
