namespace Memo.Blog.Application.Roles.Commands.Delete;

[Authorize(Permissions = ApiPermission.Role.Delete)]
public record DeleteRoleCommand(long RoleId) : IRequest<Result>;
