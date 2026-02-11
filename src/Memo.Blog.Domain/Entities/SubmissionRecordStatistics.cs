namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 提交记录汇总统计表
/// </summary>
[Table(Name = "submission_record_statistics")]
public class SubmissionRecordStatistics : BaseAuditEntity
{
    /// <summary>
    /// 统计日期
    /// </summary>
    [Description("统计日期")]
    [Column(IsNullable = false)]
    public DateTime Date { get; set; }

    /// <summary>
    /// 文章提交数
    /// </summary>
    [Description("文章提交数")]
    [Column(IsNullable = false)]
    public int Article { get; set; }

    /// <summary>
    /// 动态提交数
    /// </summary>
    [Description("动态提交数")]
    [Column(IsNullable = false)]
    public int Moment { get; set; }

    /// <summary>
    /// 笔记提交数
    /// </summary>
    [Description("笔记提交数")]
    [Column(IsNullable = false)]
    public int Note { get; set; }
}
