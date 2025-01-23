using Memo.Blog.Domain.Entities;

namespace Memo.Blog.Domain.Events.Messages;

public class MessageReplyEmailEvent : IDomainEvent
{
    /// <summary>
    /// 回复的评论
    /// </summary>
    public Comment Reply { get; set; } = new();

    /// <summary>
    /// 被回复的评论
    /// </summary>
    public Comment Source { get; set; } = new();
}
