using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Categories.Events;

public class DeletedTagEventHadler(IBaseDefaultRepository<ArticleTag> articleTagRepo) : INotificationHandler<DeletedTagEvent>
{
    public async Task Handle(DeletedTagEvent notification, CancellationToken cancellationToken)
    {
        var articleTags = await articleTagRepo.Select.Where(t => t.TagId == notification.TagId).ToListAsync(cancellationToken);
        await articleTagRepo.DeleteAsync(articleTags, cancellationToken);
    }
}
