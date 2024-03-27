namespace Memo.Blog.Application.Roles.Queries.Get;

public record GetRoleQuery(long RoleId) : IRequest<Result>;
