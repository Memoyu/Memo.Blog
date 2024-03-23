using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Events;

public class ArticleDeleteTagEventHandler(
    IMapper mapper,
    IBaseMongoRepository<ArticleCollection> articleMongoResp,
    IBaseDefaultRepository<TagArticle> tagArticleRepo,
    IBaseDefaultRepository<Tag> tagRepo
    ) : INotificationHandler<ArticleDeleteTagEvent>
{
    public async Task Handle(ArticleDeleteTagEvent notification, CancellationToken cancellationToken)
    {
        var articleTags = await tagArticleRepo.Select
            .Include(ta => ta.Tag)
            .Where(ta => ta.ArticleId == notification.ArticleId)
            .ToListAsync(cancellationToken);

        // 更新mongodb文章详情
        var tags = articleTags.Where(ta => ta.TagId != notification.TagId).ToList();

        var update = MongoDB.Driver.Builders<ArticleCollection>.Update
             .Set(nameof(ArticleCollection.Tags), mapper.Map<List<ArticleTagBson>>(tags));
        var filter = MongoDB.Driver.Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, notification.ArticleId);
        var mongoUpdate = await articleMongoResp.UpdateOneAsync(update, filter, null, cancellationToken);

    }
}
