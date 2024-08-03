namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章模板表
/// </summary>
[Table(Name = "article_template")]
[Index("index_on_template_id", nameof(TemplateId), false)]
public class ArticleTemplate : BaseAuditEntity
{
    /// <summary>
    /// 模板Id
    /// </summary>
    [Snowflake]
    [Description("文章Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long TemplateId { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    [Description("模板名称")]
    [Column(StringLength = 40, IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 模板正文
    /// </summary>
    [Description("模板正文")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Content { get; set; } = string.Empty;
}
