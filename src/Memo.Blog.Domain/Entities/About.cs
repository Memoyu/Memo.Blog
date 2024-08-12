namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 关于表
/// </summary>
[Table(Name = "about")]
public class About : BaseAuditEntity
{
    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    [Column(StringLength = 200, IsNullable = false)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 个人标签
    /// </summary>
    [Description("个人标签")]
    [Column(StringLength = 300, IsNullable = false)]
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [Description("内容")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 是否开启评论
    /// </summary>
    [Description("是否开启评论")]
    [Column(IsNullable = false)]
    public bool Commentable { get; set; }
}
