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
    [Column(CanUpdate = false)]
    public long MomentId { get; set; }

    /// <summary>
    /// 动态标签
    /// </summary>
    [Description("动态标签")]
    [Column(StringLength = 300)]
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// 动态内容
    /// </summary>
    [Description("动态内容")]
    [Column(StringLength = -2)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 点赞次数
    /// </summary>
    [Description("点赞次数")]
    public int Likes { get; set; }

    /// <summary>
    /// 是否展示
    /// </summary>
    [Description("是否展示")]
    public bool Showable { get; set; }

    /// <summary>
    /// 是否开启评论
    /// </summary>
    [Description("是否开启评论")]
    public bool Commentable { get; set; }
}
