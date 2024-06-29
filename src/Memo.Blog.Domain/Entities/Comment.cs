using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 评论表
/// </summary>
[Table(Name = "comment")]
[Index("index_on_comment_id", nameof(CommentId), false)]
public class Comment : BaseAuditEntity
{
    /// <summary>
    /// 评论Id
    /// </summary>
    [Snowflake]
    [Description("评论Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long CommentId { get; set; }

    /// <summary>
    /// 父评论Id
    /// </summary>
    [Description("父评论Id")]
    public long? ParentId { get; set; }

    /// <summary>
    /// 回复评论Id
    /// </summary>
    [Description("回复评论Id")]
    public long? ReplyId { get; set; }

    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    [Description("所属Id（文章Id、动态Id等）")]
    [Column(IsNullable = false)]
    public long BelongId { get; set; }

    /// <summary>
    /// 评论所属类型
    /// </summary>
    [Description("评论所属类型")]
    [Column(IsNullable = false)]
    public BelongType CommentType { get; set; }

    /// <summary>
    /// 访客Id
    /// </summary>
    [Description("访客Id")]
    [Column(IsNullable = false)]
    public long VisitorId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [Description("评论内容")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 评论所在IP
    /// </summary>
    [Description("评论所在IP")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 评论IP所属
    /// </summary>
    [Description("评论IP所属")]
    [Column(StringLength = 100, IsNullable = false)]
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 楼层
    /// </summary>
    [Description("楼层")]
    [Column(IsNullable = false)]
    public int Floor { get; set; }

    /// <summary>
    /// 是否展示
    /// </summary>
    [Description("是否展示")]
    [Column(IsNullable = false)]
    public bool Showable { get; set; }

    /// <summary>
    /// 访客信息
    /// </summary>
    [Navigate(nameof(Visitor.VisitorId), TempPrimary = nameof(VisitorId))]
    public virtual Visitor Visitor { get; set; } = new();
}
