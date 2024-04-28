using Memo.Blog.Application.Visitors.Common;

namespace Memo.Blog.Application.Visitors.Queries.Get;

public class GetVisitorQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Visitor> visitorRepo) : IRequestHandler<GetVisitorQuery, Result>
{
    public async Task<Result> Handle(GetVisitorQuery request, CancellationToken cancellationToken)
    {
        var visitor = await visitorRepo.Select.Where(f => f.VisitorId == request.VisitorId).FirstAsync(cancellationToken);
        return visitor is null ? throw new ApplicationException("访客不存在") : Result.Success(mapper.Map<VisitorResult>(visitor));
    }
}
