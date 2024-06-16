namespace Memo.Blog.Domain.Enums;

/// <summary>
/// 消息类型
/// </summary>
public enum MessageType
{
    [Description("用户消息")]
    User = 0,

    [Description("评论")]
    Comment = 1,

    [Description("点赞")]
    Like = 2,

    [Description("通知")]
    Notify = 3,
}
