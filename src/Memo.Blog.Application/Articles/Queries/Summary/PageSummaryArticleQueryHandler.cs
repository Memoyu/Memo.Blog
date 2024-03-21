using Memo.Blog.Application.Articles.Common;

namespace Memo.Blog.Application.Articles.Queries.Summary;

public class PageSummaryArticleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageSummaryArticleQuery, Result>
{
    public async Task<Result> Handle(PageSummaryArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = await articleRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Title), a => a.Title.Contains(request.Title!))
            .WhereIf(request.CategoryId > 0, a => a.CategoryId == request.CategoryId)
            .WhereIf(request.TagIds != null && request.TagIds.Any(), a => a.TagArticles.Any(ta => request.TagIds!.Contains(ta.TagId)))
            .WhereIf(request.Status.HasValue, a => a.Status == request.Status!.Value)
            .ToListAsync(a => new{ a.ArticleId, a.Views });

        var articleTotal = articles.Count;

        var viewTotal = articles.Sum(a => a.Views);

        var commentTotal = (int)await commentRepo.Select
           .Where(c => articles.Any(a => a.ArticleId == c.BelongId)).CountAsync();


        var dto = new PageSummaryArticleResult(articleTotal, commentTotal, viewTotal);
        return Result.Success(dto);
    }
}
