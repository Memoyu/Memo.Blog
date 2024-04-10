namespace Memo.Blog.Application.Anlyanis.Common;

public record SummaryAnlyanisResult
{
    /// <summary>
    /// 本周文章数
    /// </summary>
    public int WeekArticles { get; set; }

    /// <summary>
    /// 文章数
    /// </summary>
    public int Articles { get; set; }

    /// <summary>
    /// 动态总数
    /// </summary>
    public int Moments { get; set; }

    /// <summary>
    /// 友链总数
    /// </summary>
    public int Friends { get; set; }
}
