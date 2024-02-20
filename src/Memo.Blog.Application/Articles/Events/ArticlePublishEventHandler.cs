using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Events;

public class ArticlePublishEventHandler : INotificationHandler<ArticlePublishEvent>
{
    public async Task Handle(ArticlePublishEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
