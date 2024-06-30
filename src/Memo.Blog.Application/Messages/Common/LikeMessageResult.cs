namespace Memo.Blog.Application.Messages.Common;

public record LikeMessageResult : LikeMessageContent
{
    /// <summary>
    /// 访客昵称
    /// </summary>
    public string VisitorNickname { get; set; } = string.Empty;

    /// <summary>
    /// 访客头像
    /// </summary>
    public string VisitorAvatar { get; set; } = string.Empty;

    /// <summary>
    /// 内容标题
    /// </summary>
    public string Title { get; set; } = string.Empty;
}
