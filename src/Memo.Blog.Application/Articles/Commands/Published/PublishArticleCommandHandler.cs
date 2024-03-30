using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Commands.Published;

public class PublishArticleCommandHandler(
    IBaseDefaultRepository<Article> articleRepo,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo
    ) : IRequestHandler<PublishArticleCommand, Result>
{
    public async Task<Result> Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepo.Select.Where(t => t.ArticleId == request.ArticleId).FirstAsync(cancellationToken);
        if (article == null) throw new ApplicationException("文章不存在");

        article.AddDomainEvent(new PublishedArticleEvent(article.ArticleId));

        article.Status = Domain.Enums.ArticleStatus.Published;
        var affrows = await articleRepo.UpdateAsync(article, cancellationToken);

        if (affrows <= 0) throw new ApplicationException("发布文章失败");

        var update = Builders<ArticleCollection>.Update
            .Set(nameof(article.Status), Domain.Enums.ArticleStatus.Published);
        var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, article.ArticleId);
        var updateMongo = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);

        return updateMongo.IsAcknowledged ? Result.Success() : throw new ApplicationException("发布文章失败");
    }
}

