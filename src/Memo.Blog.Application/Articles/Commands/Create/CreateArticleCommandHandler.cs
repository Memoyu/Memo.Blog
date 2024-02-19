using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Domain.Entities.Mongo;

namespace Memo.Blog.Application.Articles.Commands.Create;

public class CreateArticleCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Article> _articleResp,
    IBaseDefaultRepository<TagArticle> _tagArticleResp,
    IBaseMongoRepository<ArticleCollection> _articleMongoResp
    ) : IRequestHandler<CreateArticleCommand, Result>
{
    public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = _mapper.Map<Article>(request);
        article = await _articleResp.InsertAsync(article, cancellationToken);

        var tagArticles = request.Tags.Select(t => new TagArticle { ArticleId = article.ArticleId, TagId = t }).ToList();
        await _tagArticleResp.InsertAsync(tagArticles);

        var articleCollection = _mapper.Map<ArticleCollection>(article);
        var mongoInsert = await _articleMongoResp.InsertOneAsync(articleCollection, null, cancellationToken);
        if (!mongoInsert) throw new Exception("写入mongodb失败");

        var result = _mapper.Map<ArticleResult>(article);

        return Result.Success(result);
    }
}
