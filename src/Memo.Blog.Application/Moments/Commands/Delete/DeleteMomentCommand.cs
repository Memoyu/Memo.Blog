namespace Memo.Blog.Application.Moments.Commands.Delete;

[Authorize(Permissions = ApiPermission.Moment.Delete)]
public record DeleteMomentCommand(
    ) : IRequest<Result>;
