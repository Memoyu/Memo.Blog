using Memo.Blog.Domain.Events.Articles;
using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Categories.Events;

public class TagDeletedEventHadler(IBaseDefaultRepository<TagArticle> tagArticleRepo) : INotificationHandler<TagDeletedEvent>
{
    public async Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
    {
        var tagArticles = await tagArticleRepo.Select.Where(t => t.TagId == notification.TagId).ToListAsync(cancellationToken);
        var rows = await tagArticleRepo.DeleteAsync(ta => ta.TagId == notification.TagId, cancellationToken);

        foreach (var tagArticle in tagArticles)
        {
            tagArticle.AddDomainEvent(new ArticleUpdateTagEvent(tagArticle.ArticleId));
        }


    }
}
