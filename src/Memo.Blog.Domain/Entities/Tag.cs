namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章标签表
/// </summary>
[Table(Name = "tag")]
[Index("index_on_tag_id", nameof(TagId), false)]
public class Tag : BaseAuditEntity
{
    /// <summary>
    /// 标签Id
    /// </summary>
    [Snowflake]
    [Description("标签Id")]
    [Column(CanUpdate = false)]
    public long TagId { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    [Description("标签名称")]
    [Column(StringLength = 40)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 标签颜色
    /// </summary>
    [Description("标签颜色")]
    [Column(StringLength = 7)]
    public string Color { get; set; } = string.Empty;
}
