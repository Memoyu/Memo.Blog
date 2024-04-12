namespace Memo.Blog.Application.Anlyanis.Common;

public record PageVisitorAnlyanisResult
{
    /// <summary>
    /// 今日PV数
    /// </summary>
    public long TodayPageVisitors { get; set; }

    /// <summary>
    /// 周PV数据
    /// </summary>
    public List<MetricItemResult> WeekPageVisitors { get; set; } = [];

    /// <summary>
    /// 总PV数
    /// </summary>
    public long PageVisitors { get; set; }
}
