namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章标签表
/// </summary>
[Table(Name = "tag")]
public class Tag : BaseAuditEntity
{
    /// <summary>
    /// 标签Id
    /// </summary>
    [Snowflake]
    [Description("标签Id")]
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
    [Column(StringLength = 6)]
    public string Color { get; set; } = string.Empty;
}
