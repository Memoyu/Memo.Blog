namespace Memo.Blog.Application.Logger.Queries.Access.Page;

[Authorize(Permissions = ApiPermission.LoggerAccess.Page)]
public record PageLoggerAccessQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
}
