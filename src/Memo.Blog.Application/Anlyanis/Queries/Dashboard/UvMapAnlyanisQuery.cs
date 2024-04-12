namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;

[Authorize(Permissions = ApiPermission.Anlyanis.UniqueVisitorMap)]
public record UvMapAnlyanisQuery : IAuthorizeableRequest<Result>;

