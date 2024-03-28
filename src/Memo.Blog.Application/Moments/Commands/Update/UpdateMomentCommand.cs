namespace Memo.Blog.Application.Moments.Commands.Update;

[Authorize(Permissions = ApiPermission.Moment.Update)]
public record UpdateMomentCommand(
    ) : IRequest<Result>;
