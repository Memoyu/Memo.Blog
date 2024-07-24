using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class DeletedArticleCommentEventHandler(
    IMarkdownService markdownService,
    ISegmenterService segmenterService,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : INotificationHandler<DeletedArticleCommentEvent>
{
    public async Task Handle(DeletedArticleCommentEvent notification, CancellationToken cancellationToken)
    {
        // 整合，与更新评论的整合
        var articleCommentContents = await commentRepo.Select.Where(c => c.BelongId == notification.ArticleId && c.CommentId != notification.CommentId).ToListAsync(c => c.Content, cancellationToken);

        if (articleCommentContents == null) return;

        var removeTags = articleCommentContents.Select(markdownService.RemoveTag).ToList();
        var segs = segmenterService.CutWithSplitForSearch(string.Join(" ", removeTags));

        var update = Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Comments), segs.ToUtf8());

        var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, notification.ArticleId);
        var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        if (!mongoUpdate.IsAcknowledged) throw new Exception("更新mongodb失败");
    }
}
