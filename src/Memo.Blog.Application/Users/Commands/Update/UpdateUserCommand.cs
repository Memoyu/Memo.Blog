namespace Memo.Blog.Application.Users.Commands.Update;

[Authorize(Permissions = ApiPermission.User.Update)]
public record UpdateUserCommand(
    ) : IRequest<Result>;
