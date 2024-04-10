namespace Memo.Blog.Application.Anlyanis.Common;

public record UniqueVisitorAnlyanisResult
{
    /// <summary>
    /// 今日UV数
    /// </summary>
    public int TodayUvs { get; set; }

    /// <summary>
    /// 周UV数据
    /// </summary>
    public List<int> WeekUvs { get; set; } = [];

    /// <summary>
    /// 总UV数
    /// </summary>
    public int TotalUvs { get; set; }
}
