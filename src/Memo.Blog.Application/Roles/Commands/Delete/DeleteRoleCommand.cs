namespace Memo.Blog.Application.Roles.Commands.Delete;

public record DeleteRoleCommand(long RoleId) : IRequest<Result>;
