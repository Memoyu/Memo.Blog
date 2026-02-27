namespace Memo.Blog.Application.SubmissionRecords.Common;

public record SubmissionRecordListResult
{
    public int Total { get; set; }

    public Dictionary<string, int> Record { get; set; } = [];
}
