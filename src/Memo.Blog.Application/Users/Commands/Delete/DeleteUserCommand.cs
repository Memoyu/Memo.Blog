namespace Memo.Blog.Application.Users.Commands.Delete;

[Authorize(Permissions = ApiPermission.User.Delete)]
public record DeleteUserCommand(
    ) : IRequest<Result>;
