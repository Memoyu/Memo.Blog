namespace Memo.Blog.Application.Articles.Common;

public class ArticleResult
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 所属分类Id
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 正文
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
