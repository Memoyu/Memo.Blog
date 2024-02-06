namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 动态表
/// </summary>
[Table(Name = "moment")]
public class Moment : BaseAuditEntity
{
    /// <summary>
    /// 动态Id
    /// </summary>
    [Snowflake]
    [Description("动态Id")]
    public long MomentId { get; set; }

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
}
