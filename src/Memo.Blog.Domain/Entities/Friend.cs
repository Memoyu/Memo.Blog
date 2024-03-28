namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 友链表
/// </summary>
[Table(Name = "friend")]
[Index("index_on_friend_id", nameof(FriendId), false)]
public class Friend : BaseAuditEntity
{
    /// <summary>
    /// 友链Id
    /// </summary>
    [Snowflake]
    [Description("友链Id")]
    [Column(CanUpdate = false)]
    public long FriendId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [Description("昵称")]
    [Column(StringLength = 50)]
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [Description("描述")]
    [Column(StringLength = 200)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 站点链接
    /// </summary>
    [Description("站点链接")]
    public string Site { get; set; } = string.Empty;

    /// <summary>
    ///  头像url
    /// </summary>
    [Description("头像url")]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 是否展示
    /// </summary>
    [Description("是否展示")]
    public bool Showable { get; set; }

    /// <summary>
    /// 点击访问次数
    /// </summary>
    [Description("点击访问次数")]
    public int Views { get; set; }
}
