namespace Memo.Blog.Application.Moments.Queries.Page;

[Authorize(Permissions = ApiPermission.Moment.Page)]
public record PageMomentQuery(
    List<string>? Tags,
    string? Content,
    DateTime? DateBegin,
    DateTime? DateEnd
    ) : PaginationQuery, IAuthorizeableRequest<Result>;
