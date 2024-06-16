using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 消息表
/// </summary>
[Table(Name = "message")]
[Index("index_on_message_id", nameof(MessageId), false)]
public class Message : BaseAuditEntity
{
    /// <summary>
    /// 消息Id
    /// </summary>
    [Snowflake]
    [Description("消息Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long MessageId { get; set; }

    /// <summary>
    /// 发送Id
    /// </summary>
    [Description("发送Id")]
    [Column(IsNullable = false)]
    public long FromId { get; set; }

    /// <summary>
    /// 接收Id
    /// </summary>
    [Description("接收Id")]
    [Column(IsNullable = false)]
    public long ToId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    [Description("消息类型")]
    [Column(IsNullable = false)]
    public MessageType MessageType { get; set; }

    /// <summary>
    /// 消息内容（结构化数据）
    /// </summary>
    [Description("消息内容")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 是否已读
    /// </summary>
    [Description("是否已读")]
    [Column(IsNullable = false)]
    public bool IsRead { get; set; }
}
