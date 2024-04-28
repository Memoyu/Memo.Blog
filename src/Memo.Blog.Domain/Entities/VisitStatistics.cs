namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 访问汇总统计表
/// </summary>
[Table(Name = "visit_statistics")]
public class VisitStatistics : BaseAuditEntity
{
    /// <summary>
    /// 统计日期
    /// </summary>
    [Description("统计日期")]
    [Column(IsNullable = false)]
    public DateTime Date { get; set; }

    /// <summary>
    /// 当日UV总量
    /// </summary>
    [Description("当日UV总数")]
    [Column(IsNullable = false)]
    public long UniqueVisitors { get; set; }

    [Description("当日PV总数")]
    [Column(IsNullable = false)]
    public long PageVisitors { get; set; }
}
