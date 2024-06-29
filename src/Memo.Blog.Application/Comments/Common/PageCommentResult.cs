using Memo.Blog.Application.Visitors.Common;
using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Common;

public record PageCommentResult
{
    /// <summary>
    /// 评论Id
    /// </summary>
    public long CommentId { get; set; }

    /// <summary>
    /// 所属信息（文章、动态等）
    /// </summary>
    public CommentBelongResult Belong { get; set; } = new();

    /// <summary>
    /// 评论类型
    /// </summary>
    public BelongType CommentType { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 访客信息
    /// </summary>
    public VisitorResult Visitor { get; set; } = new();

    /// <summary>
    /// 回复的评论
    /// </summary>
    public CommentReplyResult? Reply { get; set; }

    /// <summary>
    /// 子评论
    /// </summary>
    public List<PageCommentResult> Children { get; set; } = [];

    /// <summary>
    /// 评论所在IP
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 评论IP所属
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 是否展示
    /// </summary>
    public bool Showable { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

}
