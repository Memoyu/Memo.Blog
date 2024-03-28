using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class DeletedArticleEventHandler(
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : INotificationHandler<DeletedArticleEvent>
{
    public async Task Handle(DeletedArticleEvent notification, CancellationToken cancellationToken)
    {
        // 删除关联标签
        var articleTags = await articleTagRepo.Select.Where(t => t.ArticleId == notification.ArticleId).ToListAsync(cancellationToken);
        await articleTagRepo.DeleteAsync(articleTags, cancellationToken);

        // 删除评论
        var comments = await commentRepo.Select.Where(t => t.BelongId == notification.ArticleId).ToListAsync(cancellationToken);
        await commentRepo.DeleteAsync(comments, cancellationToken);

        // 删除Mongo文章数据
        FilterDefinitionBuilder<ArticleCollection> buildFilter = Builders<ArticleCollection>.Filter;
        var filter = buildFilter.Eq(a => a.ArticleId, notification.ArticleId);
        var deleteResult = await articleMongoRepo.DeleteOneAsync(filter, null, cancellationToken);
        if (deleteResult?.IsAcknowledged != true) throw new Exception();
    }
}
