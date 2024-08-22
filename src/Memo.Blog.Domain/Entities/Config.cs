namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章标签表
/// </summary>
[Table(Name = "config")]
public class Config : BaseAuditEntity
{
    /// <summary>
    /// 管理员访客身份配置
    /// </summary>
    [Description("管理员访客身份配置")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Visitors { get; set; } = string.Empty;

    /// <summary>
    /// 头图配置
    /// </summary>
    [Description("头图配置")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Banner { get; set; } = string.Empty;

    /// <summary>
    /// 颜色配置
    /// </summary>
    [Description("颜色配置")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Color { get; set; } = string.Empty;
}
