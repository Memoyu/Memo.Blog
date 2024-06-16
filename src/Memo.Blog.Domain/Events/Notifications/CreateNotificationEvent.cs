namespace Memo.Blog.Domain.Events.Notifications;

public record CreateNotificationEvent : IDomainEvent
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}
