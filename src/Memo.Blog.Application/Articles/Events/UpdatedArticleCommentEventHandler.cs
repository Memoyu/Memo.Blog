using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class UpdatedArticleCommentEventHandler(
    IMarkdownService markdownService,
    ISegmenterService segmenterService,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : INotificationHandler<UpdatedArticleCommentEvent>
{
    public async Task Handle(UpdatedArticleCommentEvent notification, CancellationToken cancellationToken)
    {
        var articleCommentContents = await commentRepo.Select.Where(c => c.BelongId == notification.ArticleId).ToListAsync(c => c.Content, cancellationToken);

        if (articleCommentContents == null) return;

        // 移除markdown标签
        var removeTags = articleCommentContents.Select(markdownService.RemoveTag).ToList();
        var segs = segmenterService.CutWithSplitForSearch(string.Join(" ", removeTags));

        var update = Builders<ArticleCollection>.Update
                 .Set(nameof(ArticleCollection.Comments), segs.ToUtf8());

        var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, notification.ArticleId);
        var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        if (!mongoUpdate.IsAcknowledged) throw new Exception("更新mongodb失败");
    }
}
