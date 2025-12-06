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
    /// 文章横幅图
    /// </summary>
    public string Banner { get; set; } = string.Empty;

    /// <summary>
    /// 评论条数
    /// </summary>
    public int Comments { get; set; }

    /// <summary>
    /// 浏览次数
    /// </summary>
    public int Views { get; set; }

    /// <summary>
    /// 点赞次数
    /// </summary>
    public int Likes { get; set; }
}
