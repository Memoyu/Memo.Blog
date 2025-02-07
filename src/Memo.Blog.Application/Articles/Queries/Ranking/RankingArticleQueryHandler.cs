﻿using Memo.Blog.Application.Articles.Common;

namespace Memo.Blog.Application.Articles.Queries.Ranking;

public class RankingArticleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo
    ) : IRequestHandler<RankingArticleQuery, Result>
{
    public async Task<Result> Handle(RankingArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = await articleRepo.Select
            .IncludeMany(a => a.ArticleComments)
            .IncludeMany(a => a. ArticleLikes)
            .Where(a => a.Publicable)
            .Where(a => a.Status == Domain.Enums.ArticleStatus.Published)
            .OrderByDescending(a => new { a.Views, Likes = a. ArticleLikes.Count, Comments = a.ArticleComments.Count, a.CreateTime })
            .Page(1, request.Quota)
            .ToListAsync(cancellationToken);

        return Result.Success(mapper.Map<List<RankingArticleResult>>(articles ?? []));
    }
}
