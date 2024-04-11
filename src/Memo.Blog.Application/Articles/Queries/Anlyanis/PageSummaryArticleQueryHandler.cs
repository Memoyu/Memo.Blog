﻿using Memo.Blog.Application.Articles.Common;

namespace Memo.Blog.Application.Articles.Queries.Anlyanis;

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
            .WhereIf(request.TagIds != null && request.TagIds.Count != 0, a => a.ArticleTags.Any(at => request.TagIds!.Contains(at.TagId)))
            .WhereIf(request.Status.HasValue, a => a.Status == request.Status!.Value)
            .ToListAsync(a => new{ a.ArticleId, a.Views }, cancellationToken);

        var articleTotal = articles.Count;

        var viewTotal = articles.Sum(a => a.Views);

        var commentTotal = (int)await commentRepo.Select
           .Where(c => articles.Any(a => a.ArticleId == c.BelongId)).CountAsync(cancellationToken);

        var dto = new PageSummaryArticleResult(articleTotal, commentTotal, viewTotal);
        return Result.Success(dto);
    }
}
