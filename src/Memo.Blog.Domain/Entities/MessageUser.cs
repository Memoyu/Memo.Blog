namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 消息接收方表
/// </summary>
[Table(Name = "message_user")]
[Index("index_on_message_user_message_id", nameof(MessageId), false)]
[Index("index_on_message_user_user_id", nameof(UserId), false)]
public class MessageUser : BaseAuditEntity
{
    /// <summary>
    /// 消息Id
    /// </summary>
    [Description("消息Id")]
    [Column(IsNullable = false)]
    public long MessageId { get; set; }

    /// <summary>
    /// 接收方Id
    /// </summary>
    [Description("接收方Id")]
    [Column(IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 是否已读
    /// </summary>
    [Description("是否已读")]
    [Column(IsNullable = false)]
    public bool IsRead { get; set; }
}
