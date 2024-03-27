using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Events;

public class ArticleUpdateTagEventHandler(
    IMapper mapper,
    IBaseMongoRepository<ArticleCollection> articleMongoResp,
    IBaseDefaultRepository<TagArticle> tagArticleRepo,
    IBaseDefaultRepository<Tag> tagRepo
    ) : INotificationHandler<ArticleUpdateTagEvent>
{
    public async Task Handle(ArticleUpdateTagEvent notification, CancellationToken cancellationToken)
    {
        var articleTags = await tagArticleRepo.Select
            .Include(ta => ta.Tag)
            .Where(ta => ta.ArticleId == notification.ArticleId)
            .ToListAsync(cancellationToken);

        // 更新mongodb文章详情
        var tags = articleTags.Select(at => at.Tag).ToList();

        var update = MongoDB.Driver.Builders<ArticleCollection>.Update
             .Set(nameof(ArticleCollection.Tags), mapper.Map<List<ArticleTagBson>>(tags));
        var filter = MongoDB.Driver.Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, notification.ArticleId);
        var mongoUpdate = await articleMongoResp.UpdateOneAsync(update, filter, null, cancellationToken);

    }
}
