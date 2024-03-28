namespace Memo.Blog.Application.Moments.Queries.Page;

[Authorize(Permissions = ApiPermission.Moment.Page)]
public record PageMomentQuery(
    ) : PaginationQuery, IRequest<Result>;
