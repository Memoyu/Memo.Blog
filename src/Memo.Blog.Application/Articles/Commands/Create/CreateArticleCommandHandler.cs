using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Domain.Entities.Mongo;

namespace Memo.Blog.Application.Articles.Commands.Create;

public class CreateArticleCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleResp,
    IBaseDefaultRepository<TagArticle> tagArticleResp,
    IBaseMongoRepository<ArticleCollection> articleMongoResp
    ) : IRequestHandler<CreateArticleCommand, Result>
{
    public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = mapper.Map<Article>(request);
        article = await articleResp.InsertAsync(article, cancellationToken);

        var tagArticles = request.Tags.Select(t => new TagArticle { ArticleId = article.ArticleId, TagId = t }).ToList();
        await tagArticleResp.InsertAsync(tagArticles);

        var articleCollection = mapper.Map<ArticleCollection>(article);
        var mongoInsert = await articleMongoResp.InsertOneAsync(articleCollection, null, cancellationToken);
        if (!mongoInsert) throw new Exception("写入mongodb失败");

        var result = mapper.Map<ArticleResult>(article);

        return Result.Success(result);
    }
}
