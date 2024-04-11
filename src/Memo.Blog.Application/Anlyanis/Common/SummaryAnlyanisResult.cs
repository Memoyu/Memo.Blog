namespace Memo.Blog.Application.Anlyanis.Common;

public record SummaryAnlyanisResult
{
    /// <summary>
    /// 本周文章数
    /// </summary>
    public long WeekArticles { get; set; }

    /// <summary>
    /// 文章数
    /// </summary>
    public long Articles { get; set; }

    /// <summary>
    /// 动态总数
    /// </summary>
    public long Moments { get; set; }

    /// <summary>
    /// 友链总数
    /// </summary>
    public long Friends { get; set; }
}
