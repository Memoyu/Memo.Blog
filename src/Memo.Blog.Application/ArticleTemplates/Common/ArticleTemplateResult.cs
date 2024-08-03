namespace Memo.Blog.Application.ArticleTemplates.Common;

public record ArticleTemplateResult
{
    /// <summary>
    /// 模板Id
    /// </summary>
    public long TemplateId { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 模板正文
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
