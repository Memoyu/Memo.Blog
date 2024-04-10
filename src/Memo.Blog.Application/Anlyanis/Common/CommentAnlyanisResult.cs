namespace Memo.Blog.Application.Anlyanis.Common;

public record CommentAnlyanisResult
{
    /// <summary>
    /// 今日评论数
    /// </summary>
    public int TodayComments { get; set; }

    /// <summary>
    /// 周评论数据
    /// </summary>
    public List<int> WeekComments { get; set; } = [];

    /// <summary>
    /// 总评论数
    /// </summary>
    public int TotalComments { get; set; }
}
