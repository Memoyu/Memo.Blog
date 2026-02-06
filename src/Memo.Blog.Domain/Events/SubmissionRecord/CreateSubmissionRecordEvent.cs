using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Events.SubmissionRecord;

/// <summary>
/// 创建提交记录
/// </summary>
/// <param name="TargetId">提交数据id</param>
/// <param name="Type">提交数据类型</param>
/// <param name="Operate">操作类型</param>
public record CreateSubmissionRecordEvent(long TargetId, SubmissionRecordType Type, SubmissionRecordOperate Operate) : IDomainEvent;
