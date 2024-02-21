using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Commands.Published;

public class PublishArticleCommandHandler(
    IBaseDefaultRepository<Article> articleResp,
    IBaseMongoRepository<ArticleCollection> articleMongoResp
    ) : IRequestHandler<PublishArticleCommand, Result>
{
    public async Task<Result> Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await articleResp.Select.Where(t => t.ArticleId == request.ArticleId).ToOneAsync(cancellationToken);
        if (article == null) return Result.Failure("文章不存在");

        article.AddDomainEvent(new ArticlePublishEvent(article.ArticleId));

        article.Status = Domain.Enums.ArticleStatus.Published;
        var rows = await articleResp.UpdateAsync(article, cancellationToken);

        if (rows <= 0) return Result.Failure("发布文章失败");

        var update = Builders<ArticleCollection>.Update
            .Set(nameof(article.Status), Domain.Enums.ArticleStatus.Published);
        var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, article.ArticleId);
        var updateMongo = await articleMongoResp.UpdateOneAsync(update, filter, null, cancellationToken);

        return rows > 0 ? Result.Success() : Result.Failure("发布文章失败");
    }
}

