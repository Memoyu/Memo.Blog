namespace Memo.Blog.Application.Configs.Queries.Get;

[Authorize(Permissions = ApiPermission.Config.Get)]
public record GetConfigQuery: IAuthorizeableRequest<Result>;

[Authorize(Permissions = ApiPermission.Config.GetAdmin)]
public record GetConfigAdminQuery : IAuthorizeableRequest<Result>;

public record GetConfigClientQuery : IRequest<Result>;
