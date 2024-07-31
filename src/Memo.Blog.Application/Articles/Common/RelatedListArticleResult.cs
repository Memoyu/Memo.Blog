namespace Memo.Blog.Application.Articles.Common;

public class RelatedListArticleResult
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    public required ArticleAuthorResult Author { get; set; }
}
