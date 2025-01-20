using Memo.Blog.Domain.Entities;

namespace Memo.Blog.Domain.Events.Messages;

public class MessageReplyEmailEvent : IDomainEvent
{
    /// <summary>
    /// 发出评论访客Id
    /// </summary>
    public long VisitorId { get; set; }

    /// <summary>
    /// 回复对应访客
    /// </summary>
    public Visitor Reply { get; set; } = new();
    
    public string Content { get; set; } = string.Empty;
}
