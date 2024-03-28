namespace Memo.Blog.Application.Moments.Queries.Get;

[Authorize(Permissions = ApiPermission.Moment.Get)]
public record GetMomentQuery(long MomentId) : IRequest<Result>;
