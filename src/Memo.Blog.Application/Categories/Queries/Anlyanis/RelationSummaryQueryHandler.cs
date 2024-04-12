using Memo.Blog.Application.Anlyanis.Common;

namespace Memo.Blog.Application.Categories.Queries.Anlyanis;

public class RelationSummaryQueryHandler(
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<RelationSummaryQuery, Result>
{
    public async Task<Result> Handle(RelationSummaryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepo.Select.ToListAsync(cancellationToken);
        var categoryIds = categories.Select(c => c.CategoryId).ToList();
        var articles = await articleRepo.Select
            .Where(a => categoryIds.Contains(a.CategoryId))
            .ToListAsync(a => new { a.ArticleId, a.CategoryId }, cancellationToken);

        var result = categories.Select(c => new MetricItemResult(c.Name, articles.Count(a => a.CategoryId == c.CategoryId))).ToList();

        return Result.Success(result);
    }
}
