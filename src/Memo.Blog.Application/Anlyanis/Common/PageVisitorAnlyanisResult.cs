namespace Memo.Blog.Application.Anlyanis.Common;

public record PageVisitorAnlyanisResult
{
    /// <summary>
    /// 今日PV数
    /// </summary>
    public int TodayPvs { get; set; }

    /// <summary>
    /// 周PV数据
    /// </summary>
    public List<int> WeekPvs { get; set; } = [];

    /// <summary>
    /// 总PV数
    /// </summary>
    public int TotalPvs { get; set; }
}
