using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Events.Messages;

public record MessageUserEmailEvent : IDomainEvent
{
    public long FromUser { get; set; }

    public List<long> ToUsers { get; set; } = [];

    public MessageType Type { get; set; }

    public string Content { get; set; } = string.Empty;
}
