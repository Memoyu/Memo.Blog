using Memo.Blog.Domain.Entities;
using Memo.Blog.Domain.Events.Articles;
using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Categories.Events;

public class TagDeletedEventHadler(IBaseDefaultRepository<TagArticle> tagArticleRepo) : INotificationHandler<TagDeletedEvent>
{
    public async Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
    {
        var tagArticles = await tagArticleRepo.Select.Where(t => t.TagId == notification.TagId).ToListAsync(cancellationToken);

        foreach (var tagArticle in tagArticles)
        {
            tagArticle.AddDomainEvent(new ArticleUpdateTagEvent(tagArticle.ArticleId));
        }

        await tagArticleRepo.DeleteAsync(tagArticles, cancellationToken);
    }
}
