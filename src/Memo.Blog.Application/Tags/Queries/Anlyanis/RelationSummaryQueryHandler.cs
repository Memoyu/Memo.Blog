using Memo.Blog.Application.Anlyanis.Common;

namespace Memo.Blog.Application.Tags.Queries.Anlyanis;

public class RelationSummaryQueryHandler(
    IBaseDefaultRepository<ArticleTag> articleTagRepo,
    IBaseDefaultRepository<Tag> tagRepo
    ) : IRequestHandler<RelationSummaryQuery, Result>
{
    public async Task<Result> Handle(RelationSummaryQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepo.Select.ToListAsync(cancellationToken);
        var tagIds = tags.Select(c => c.TagId).ToList();
        var articles = await articleTagRepo.Select
            .Where(a => tagIds.Contains(a.TagId))
            .ToListAsync(a => new { a.ArticleId, a.TagId }, cancellationToken);

        var result = tags.Select(c => new MetricItemResult(c.Name, articles.Count(a => a.TagId == c.TagId))).ToList();

        return Result.Success(result);
    }
}
