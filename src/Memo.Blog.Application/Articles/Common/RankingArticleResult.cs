namespace Memo.Blog.Application.Articles.Common;

public class RankingArticleResult
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
    /// 浏览次数
    /// </summary>
    public int Views { get; set; }

    /// <summary>
    /// 点赞次数
    /// </summary>
    public int Likes { get; set; }
}
