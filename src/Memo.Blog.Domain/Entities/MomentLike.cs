namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 动态点赞
/// </summary>
[Table(Name = "moment_like")]
[Index("index_on_visitor_id", nameof(VisitorId), false)]
[Index("index_on_moment_id", nameof(MomentId), false)]
public class MomentLike : BaseAuditEntity
{
    /// <summary>
    /// 动态Id
    /// </summary>
    [Description("动态Id")]
    [Column(IsNullable = false)]
    public long MomentId { get; set; }

    /// <summary>
    /// 访客Id
    /// </summary>
    [Description("访客Id")]
    [Column(IsNullable = false)]
    public long VisitorId { get; set; }

    /// <summary>
    /// 访客
    /// </summary>
    [Navigate(nameof(Visitor.VisitorId), TempPrimary = nameof(VisitorId))]
    public virtual Visitor Visitor { get; set; } = new();

    /// <summary>
    /// 动态
    /// </summary>
    [Navigate(nameof(Moment.MomentId), TempPrimary = nameof(MomentId))]
    public virtual Moment Moment { get; set; } = new();
}
