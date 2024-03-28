using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Events;

public class PublishedArticleEventHandler : INotificationHandler<PublishedArticleEvent>
{
    public async Task Handle(PublishedArticleEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
