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
    [Column(StringLength = 200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [Description("内容")]
    [Column(StringLength = -2)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 是否开启评论
    /// </summary>
    [Description("是否开启评论")]
    public bool Commentable { get; set; }
}
