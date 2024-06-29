namespace Memo.Blog.Domain.Events.Messages;

public record MessageNotificationEvent : IDomainEvent
{
    public long? ToId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}
