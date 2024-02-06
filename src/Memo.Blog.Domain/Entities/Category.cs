namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章分类表
/// </summary>
[Table(Name = "category")]
public class Category : BaseAuditEntity
{
    /// <summary>
    /// 分类Id
    /// </summary>
    [Snowflake]
    [Description("分类Id")]
    public long CategoryId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [Description("分类名称")]
    [Column(StringLength = 40)]
    public string Name { get; set; } = string.Empty;
}
