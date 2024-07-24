
namespace Memo.Blog.Application.Articles.Common;

public class SearchArticlePageResult : PaginationResult<SearchArticleResult>
{
    public SearchArticlePageResult(IReadOnlyList<SearchArticleResult> items, long total) : base(items, total)
    {
    }

    public List<string> KeyWordSegs { get; set; } = [];
}


public class SearchArticleResult
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 所属分类
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 文章正文
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 关联标签标签名，空格分割
    /// </summary>
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// 评论原文，追加，空格分割
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    public DateTime CreateTime { get; set; }
}
