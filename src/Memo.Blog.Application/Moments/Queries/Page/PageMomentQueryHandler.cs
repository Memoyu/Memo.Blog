namespace Memo.Blog.Application.Moments.Queries.Page;

public class PageMomentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<PageMomentQuery, Result>
{
    public async Task<Result> Handle(PageMomentQuery request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
}
