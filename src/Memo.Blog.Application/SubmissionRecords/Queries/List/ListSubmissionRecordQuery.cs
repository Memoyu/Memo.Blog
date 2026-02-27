namespace Memo.Blog.Application.SubmissionRecords.Queries.List;

[Authorize(Permissions = ApiPermission.SubmissionRecord.List)]
public record ListSubmissionRecordQuery(int Year) : IAuthorizeableRequest<Result>;

public class ListSubmissionRecordQueryValidator : AbstractValidator<ListSubmissionRecordQuery>
{
    public ListSubmissionRecordQueryValidator()
    {
    }
}


