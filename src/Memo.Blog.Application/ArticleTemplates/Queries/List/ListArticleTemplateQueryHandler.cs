using Memo.Blog.Application.ArticleTemplates.Common;

namespace Memo.Blog.Application.ArticleTemplates.Queries.List;

public class ListArticleTemplateQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<ArticleTemplate> articleTemplateRepo
    ) : IRequestHandler<ListArticleTemplateQuery, Result>
{
    public async Task<Result> Handle(ListArticleTemplateQuery request, CancellationToken cancellationToken)
    {
        var templates = await articleTemplateRepo.Select
           .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
           .OrderByDescending(c => c.CreateTime)
           .ToListAsync(cancellationToken) ?? [];

        return Result.Success(mapper.Map<List<ArticleTemplateResult>>(templates));
    }
}

