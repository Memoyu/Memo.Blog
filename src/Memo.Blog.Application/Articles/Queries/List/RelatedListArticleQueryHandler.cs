using Memo.Blog.Application.Articles.Common;

namespace Memo.Blog.Application.Articles.Queries.List;

public class RelatedListArticleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo
    ) : IRequestHandler<RelatedListArticleQuery, Result>
{
    public async Task<Result> Handle(RelatedListArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = new List<Article>();
        if (request.Type == 1)
        {
            articles = await articleRepo.Select
               .Where(a => a.CategoryId == request.Id)
               .OrderByDescending(a => a.CreateTime)
               .ToListAsync(cancellationToken);
        }
        else if (request.Type == 2)
        {
            articles = await articleTagRepo.Select
                .Include(at => at.Article)
                .Where(at => at.TagId == request.Id).ToListAsync(at => at.Article, cancellationToken);
        }

        var results = mapper.Map<List<RelatedListArticleResult>>(articles);
      
        return Result.Success(results);
    }
}
