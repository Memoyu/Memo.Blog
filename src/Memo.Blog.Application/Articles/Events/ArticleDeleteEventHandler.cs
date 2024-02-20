using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class ArticleDeleteEventHandler(
    IBaseMongoRepository<ArticleCollection> articleMongoResp,
    IBaseDefaultRepository<TagArticle> tagArticleRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : INotificationHandler<ArticleDeleteEvent>
{
    public async Task Handle(ArticleDeleteEvent notification, CancellationToken cancellationToken)
    {
        // 删除关联标签
        var tagArticles = await tagArticleRepo.Select.Where(t => t.ArticleId == notification.ArticleId).ToListAsync(cancellationToken);
        await tagArticleRepo.DeleteAsync(tagArticles, cancellationToken);

        // 删除评论
        var comments = await commentRepo.Select.Where(t => t.BelongId == notification.ArticleId).ToListAsync(cancellationToken);
        await commentRepo.DeleteAsync(comments, cancellationToken);

        // 删除Mongo文章数据
        FilterDefinitionBuilder<ArticleCollection> buildFilter = Builders<ArticleCollection>.Filter;
        var filter = buildFilter.Eq(a => a.ArticleId, notification.ArticleId);
        var deleteResult = await articleMongoResp.DeleteOneAsync(filter, null, cancellationToken);
        if (deleteResult?.IsAcknowledged != true) throw new Exception();
    }
}
