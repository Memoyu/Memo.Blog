using Memo.Blog.Application.Moments.Common;

namespace Memo.Blog.Application.Moments.Queries.Get;

public class GetMomentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<GetMomentQuery, Result>
{
    public async Task<Result> Handle(GetMomentQuery request, CancellationToken cancellationToken)
    {
        var moment = await momentRepo.Select.Where(f => f.MomentId == request.MomentId).FirstAsync(cancellationToken);

        return moment is null ? throw new ApplicationException("动态不存在") : (Result)Result.Success(mapper.Map<MomentResult>(moment));
    }
}
