namespace Memo.Blog.Application.Moments.Commands.Create;

[Authorize(Permissions = ApiPermission.Moment.Create)]
public record CreateMomentCommand(
    ) : IRequest<Result>;
