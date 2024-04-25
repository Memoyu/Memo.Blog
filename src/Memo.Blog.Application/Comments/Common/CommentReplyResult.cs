namespace Memo.Blog.Application.Comments.Common;

public record CommentReplyResult
{
    /// <summary>
    /// 评论Id
    /// </summary>
    public long CommentId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 楼层
    /// </summary>
    public int Floor { get; set; }

    /// <summary>
    /// 楼层
    /// </summary>
    public string FloorString { get; set; } = string.Empty;
}
