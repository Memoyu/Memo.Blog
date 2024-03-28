using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class DeletedArticleCommentEventHandler(
    IMapper mapper,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : INotificationHandler<DeletedArticleCommentEvent>
{
    public async Task Handle(DeletedArticleCommentEvent notification, CancellationToken cancellationToken)
    {
        var articleComments = await commentRepo.Select.Where(c => c.BelongId == notification.ArticleId && c.CommentId != notification.CommentId).ToListAsync<ArticleCommentBson>(cancellationToken);

        var update = Builders<ArticleCollection>.Update
                 .Set(nameof(ArticleCollection.Comments), mapper.Map<List<ArticleCommentBson>>(articleComments));

        var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, notification.ArticleId);
        var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        if (!mongoUpdate.IsAcknowledged) throw new Exception("更新mongodb失败");
    }
}
