using Memo.Blog.Application.Anlyanis.Common;
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Enums;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Queries.Anlyanis;

public class SummaryArticleQueryHandler(
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseMongoRepository<LoggerVisitCollection> loggerVisitRepo
    ) : IRequestHandler<SummaryArticleQuery, Result>
{
    public async Task<Result> Handle(SummaryArticleQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var weekBegin = now.Date.AddDays(-6); // 往后7天
        var weekEnd = now.Date; // 到昨天
        var weekRanges = weekBegin.GetRanges(weekEnd);

        var articles = await articleRepo.Select.ToListAsync(a => new { a.ArticleId, a.CreateTime }, cancellationToken);

        var f = Builders<LoggerVisitCollection>.Filter.Empty;
        f &= Builders<LoggerVisitCollection>.Filter.Eq(u => u.Behavior, VisitLogBehavior.Article);
        f &= Builders<LoggerVisitCollection>.Filter.And(
                Builders<LoggerVisitCollection>.Filter.Gte(u => u.VisitDate, weekBegin),
                Builders<LoggerVisitCollection>.Filter.Lte(u => u.VisitDate, weekEnd.AddDays(1).AddSeconds(-1)));
        var visits = await loggerVisitRepo.CountAsync(f, cancellationToken: cancellationToken);

        var comments = await commentRepo.Select
            .Where(c => c.CommentType == BelongType.Article)
            .ToListAsync(c => new { c.CommentId, c.CreateTime }, cancellationToken);

        #region 周数据分析构造

        var weekArticles = new List<MetricItemResult>();
        var weekComments = new List<MetricItemResult>();
        var weekViews = new List<MetricItemResult>();
        foreach (var date in weekRanges)
        {
            var dateStr = date.ToString("yyyy-MM-dd");

            var articleTotal = articles.Where(a => a.CreateTime.Date == date.Date).Count();
            weekArticles.Add(new MetricItemResult(dateStr, articleTotal));

            var commentTotal = comments.Where(a => a.CreateTime.Date == date.Date).Count();
            weekComments.Add(new MetricItemResult(dateStr, commentTotal));

            f = Builders<LoggerVisitCollection>.Filter.Empty;
            f &= Builders<LoggerVisitCollection>.Filter.Eq(u => u.Behavior, VisitLogBehavior.Article);
            f &= Builders<LoggerVisitCollection>.Filter.And(
                    Builders<LoggerVisitCollection>.Filter.Gte(u => u.VisitDate, date),
                    Builders<LoggerVisitCollection>.Filter.Lte(u => u.VisitDate, date.AddDays(1).AddSeconds(-1)));
            var visitTotal = await loggerVisitRepo.CountAsync(f, cancellationToken: cancellationToken);
            weekViews.Add(new MetricItemResult(dateStr, visitTotal));
        }

        #endregion

        return Result.Success(new SummaryArticleResult
        {
            Articles = articles.Count,
            Views = visits,
            Comments = comments.Count,
            WeekArticles = weekArticles,
            WeekViews = weekViews,
            WeekComments = weekComments,
        });
    }
}

public class SummaryArticleClientQueryHandler(
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Moment> memontRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<SummaryArticleClientQuery, Result>
{
    public async Task<Result> Handle(SummaryArticleClientQuery request, CancellationToken cancellationToken)
    {
        var articles = await articleRepo.Select
            .Where(a => a.Status == ArticleStatus.Published)
            .CountAsync(cancellationToken);

        var moments = await memontRepo.Select
                   .Where(c => c.Showable)
                   .CountAsync(cancellationToken);

        var comments = await commentRepo.Select
            .Where(c => c.CommentType == BelongType.Article)
            .CountAsync(cancellationToken);


        return Result.Success(new SummaryArticleClientResult
        {
            Articles = articles,
            Moments = moments,
            Comments = comments,
        });
    }
}
