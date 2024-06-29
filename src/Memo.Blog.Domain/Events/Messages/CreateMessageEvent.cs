using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Events.Messages;

public class CreateMessageEvent : IDomainEvent
{
    /// <summary>
    /// 发送方Id
    /// </summary>
    public long FromId { get; set; }

    /// <summary>
    /// 接收方Id
    /// </summary>
    public long ToId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public MessageType MessageType { get; set; }

    /// <summary>
    /// 消息内容（结构化数据）
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
