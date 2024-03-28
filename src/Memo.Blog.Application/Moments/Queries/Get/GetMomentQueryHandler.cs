namespace Memo.Blog.Application.Moments.Queries.Get;

public class GetMomentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<GetMomentQuery, Result>
{
    public Task<Result> Handle(GetMomentQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
