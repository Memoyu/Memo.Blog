namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章表
/// </summary>
[Table(Name = "article")]
public class Article : BaseAuditEntity
{
    /// <summary>
    /// 文章Id
    /// </summary>
    [Snowflake]
    [Description("文章Id")]
    public long ArticleId { get; set; }

    /// <summary>
    /// 所属分类Id
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Column(StringLength = 200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 正文
    /// </summary>
    [Column(StringLength = -2)]
    public string Content { get; set; } = string.Empty;
}
