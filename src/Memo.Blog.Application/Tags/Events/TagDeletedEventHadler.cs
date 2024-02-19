using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Categories.Events;

public class TagDeletedEventHadler(IBaseDefaultRepository<TagArticle> tagArticleRepo) : INotificationHandler<TagDeletedEvent>
{
    public async Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
    {
        var tagArticles = await tagArticleRepo.Select.Where(t => t.TagId == notification.TagId).ToListAsync(cancellationToken);

        var rows = await tagArticleRepo.DeleteAsync(tagArticles, cancellationToken);
    }
}
