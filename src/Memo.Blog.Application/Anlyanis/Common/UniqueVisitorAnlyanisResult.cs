namespace Memo.Blog.Application.Anlyanis.Common;

public record UniqueVisitorAnlyanisResult
{
    /// <summary>
    /// 今日UV数
    /// </summary>
    public long TodayUniqueVisitors { get; set; }

    /// <summary>
    /// 周UV数据
    /// </summary>
    public List<MetricItemResult> WeekUniqueVisitors { get; set; } = [];

    /// <summary>
    /// 总UV数
    /// </summary>
    public long UniqueVisitors { get; set; }
}
