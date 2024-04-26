using System.ComponentModel;
using Memo.Blog.Application.Visitors.Common;

namespace Memo.Blog.Application.Comments.Common;

public record CommentClientResult
{
    /// <summary>
    /// 父评论Id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 评论Id
    /// </summary>
    public long CommentId { get; set; }

    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    public long BelongId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 评论IP所属
    /// </summary>
    public string Region { get; set; } = string.Empty;

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

    /// <summary>
    /// 访客信息
    /// </summary>
    public VisitorClientResult Visitor { get; set; } = new();

    /// <summary>
    /// 回复的评论
    /// </summary>
    public CommentReplyResult? Reply { get; set; }

    /// <summary>
    /// 子评论
    /// </summary>
    public List<CommentClientResult> Childs { get; set; } = [];
}
