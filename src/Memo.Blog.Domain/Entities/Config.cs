namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章标签表
/// </summary>
[Table(Name = "config")]
public class Config : BaseAuditEntity
{
    /// <summary>
    /// 头图配置
    /// </summary>
    [Description("头图配置")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Banner { get; set; } = string.Empty;

    /// <summary>
    /// 样式配置
    /// </summary>
    [Description("样式配置")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Style { get; set; } = string.Empty;
}
