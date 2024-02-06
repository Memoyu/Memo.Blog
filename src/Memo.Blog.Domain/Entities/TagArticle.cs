namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章标签
/// </summary>
[Table(Name = "tag_article")]
public class TagArticle : BaseAuditEntity
{
    /// <summary>
    /// 标签Id
    /// </summary>
    [Description("标签Id")]
    public long TagId { get; set; }

    /// <summary>
    /// 文章Id
    /// </summary>
    [Description("文章Id")]
    public long ArticleId { get; set; }
}
