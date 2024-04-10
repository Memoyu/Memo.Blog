namespace Memo.Blog.Application.Logger.Queries.Visit.Page;

public class PageLoggerVisitQueryHandler(
    IMapper mapper
    ) : IRequestHandler<PageLoggerVisitQuery, Result>
{
    public async Task<Result> Handle(PageLoggerVisitQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // return Result.Success(new PaginationResult<>(results, total));
    }
}
