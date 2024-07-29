namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 友链访问记录
/// </summary>
[Table(Name = "friend_view")]
[Index("index_on_visitor_id", nameof(VisitorId), false)]
[Index("index_on_friend_id", nameof(FriendId), false)]
public class FriendView : BaseAuditEntity
{
    /// <summary>
    /// 友链Id
    /// </summary>
    [Description("友链Id")]
    [Column(IsNullable = false)]
    public long FriendId { get; set; }

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
    /// 友链
    /// </summary>
    [Navigate(nameof(Friend.FriendId), TempPrimary = nameof(FriendId))]
    public virtual Friend Friend { get; set; } = new();
}
