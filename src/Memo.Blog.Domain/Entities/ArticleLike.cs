namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章点赞
/// </summary>
[Table(Name = "article_like")]
[Index("index_on_visitor_id", nameof(VisitorId), false)]
[Index("index_on_article_id", nameof(ArticleId), false)]
public class ArticleLike : BaseAuditEntity
{
    /// <summary>
    /// 文章Id
    /// </summary>
    [Description("文章Id")]
    [Column(IsNullable = false)]
    public long ArticleId { get; set; }

    /// <summary>
    /// 访客Id
    /// </summary>
    [Description("访客Id")]
    [Column(IsNullable = false)]
    public long VisitorId { get; set; }

    /// <summary>
    /// 访客
    /// </summary>
    [Navigate(nameof(Visitor.VisitorId), TempPrimary = nameof(VisitorId))]
    public virtual Visitor Visitor { get; set; } = new();

    /// <summary>
    /// 文章
    /// </summary>
    [Navigate(nameof(Article.ArticleId), TempPrimary = nameof(ArticleId))]
    public virtual Article Article { get; set; } = new();
}
