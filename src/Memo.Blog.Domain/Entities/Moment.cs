namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 动态表
/// </summary>
[Table(Name = "moment")]
[Index("index_on_moment_id", nameof(MomentId), false)]
public class Moment : BaseAuditEntity
{
    /// <summary>
    /// 动态Id
    /// </summary>
    [Snowflake]
    [Description("动态Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long MomentId { get; set; }

    /// <summary>
    /// 动态标签
    /// </summary>
    [Description("动态标签")]
    [Column(StringLength = 300, IsNullable = false)]
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// 动态内容
    /// </summary>
    [Description("动态内容")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 点赞次数
    /// </summary>
    [Description("点赞次数")]
    [Column(IsNullable = false)]
    public int Likes { get; set; }

    /// <summary>
    /// 是否展示
    /// </summary>
    [Description("是否展示")]
    [Column(IsNullable = false)]
    public bool Showable { get; set; }

    /// <summary>
    /// 是否开启评论
    /// </summary>
    [Description("是否开启评论")]
    [Column(IsNullable = false)]
    public bool Commentable { get; set; }

    /// <summary>
    /// 动态发布者
    /// </summary>
    [Navigate(nameof(CreateUserId), TempPrimary = nameof(User.UserId))]
    public virtual User Announcer { get; set; } = new();
}
