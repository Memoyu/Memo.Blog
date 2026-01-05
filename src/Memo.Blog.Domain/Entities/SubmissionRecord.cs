using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 提交记录表
/// </summary>
[Table(Name = "submission_record")]
[Index("index_on_record_id", nameof(RecordId), false)]
public class SubmissionRecord : BaseAuditEntity
{
    /// <summary>
    /// 记录Id
    /// </summary>
    [Snowflake]
    [Description("记录Id")]
    [Column(IsNullable = false)]
    public long RecordId { get; set; }

    /// <summary>
    /// 提交类型
    /// </summary>
    [Description("提交类型")]
    [Column(IsNullable = false)]
    public SubmissionRecordType Type { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [Description("操作类型")]
    [Column(IsNullable = false)]
    public SubmissionRecordOperate Operate { get; set; }

    /// <summary>
    /// 提交目标Id
    /// </summary>
    [Description("提交目标Id")]
    [Column(IsNullable = false)]
    public long TargetId { get; set; }
}
