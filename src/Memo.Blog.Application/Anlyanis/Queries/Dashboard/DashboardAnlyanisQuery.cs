namespace Memo.Blog.Application.Anlyanis.Queries.Dashboard;

[Authorize(Permissions = ApiPermission.Anlyanis.Dashboard)]
public record DashboardAnlyanisQuery() : IAuthorizeableRequest<Result>;

public class DashboardAnlyanisQueryValidator : AbstractValidator<DashboardAnlyanisQuery>
{
}
