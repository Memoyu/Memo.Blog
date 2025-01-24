namespace Memo.Blog.Application.OpenSources.Command.Sync;

[Authorize(Permissions = ApiPermission.OpenSource.Update)]
[Transactional]
public record SynchronousCommand : IAuthorizeableRequest<Result>;

