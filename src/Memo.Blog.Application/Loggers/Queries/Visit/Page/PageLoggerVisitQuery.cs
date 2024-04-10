namespace Memo.Blog.Application.Logger.Queries.Visit.Page;

[Authorize(Permissions = ApiPermission.LoggerVisit.Page)]
public record PageLoggerVisitQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
}
