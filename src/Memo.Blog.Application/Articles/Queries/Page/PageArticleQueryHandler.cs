
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Common.Extensions;

namespace Memo.Blog.Application.Articles.Queries.Page;

public class PageArticleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
     IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageArticleQuery, Result>
{
    public async Task<Result> Handle(PageArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = await articleRepo.Select
            .Include(a => a.Category)
            .IncludeMany(a => a.ArticleTags, then => then.Include(t => t.Tag))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Title), a => a.Title.Contains(request.Title!))
            .WhereIf(request.CategoryId > 0, a => a.CategoryId == request.CategoryId)
            .WhereIf(request.TagIds != null && request.TagIds.Any(), a => a.ArticleTags.Any(at => request.TagIds!.Contains(at.TagId)))
            .WhereIf(request.Status.HasValue, a => a.Status == request.Status!.Value)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<PageArticleResult>>(articles);
        var articleIds = articles.Select(a => a.ArticleId).ToList();
        var comments = await commentRepo.Select
            .Where(c => articleIds.Contains(c.BelongId))
            .ToListAsync(c => new { c.BelongId, c.CommentId }, cancellationToken);

        foreach (var result in results)
        {
            result.Comments = comments.Where(c => c.BelongId == result.ArticleId).Count();
        }

        return Result.Success(new PaginationResult<PageArticleResult>(results, total));
    }
}

public class PageArticleClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo
    ) : IRequestHandler<PageArticleClientQuery, Result>
{
    public async Task<Result> Handle(PageArticleClientQuery request, CancellationToken cancellationToken)
    {
        var articles = await articleRepo.Select
            .Include(a => a.Category)
            .IncludeMany(a => a.ArticleTags, then => then.Include(t => t.Tag))
            .Where(a => a.Status == Domain.Enums.ArticleStatus.Published)
            .WhereIf(request.CategoryId > 0, a => a.CategoryId == request.CategoryId)
            .OrderByDescending(a => new { a.IsTop, a.CreateTime })
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<PageArticleClientResult>>(articles);

        return Result.Success(new PaginationResult<PageArticleClientResult>(results, total));
    }
}
