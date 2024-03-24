namespace Memo.Blog.Application.Logger.Queries.Access.Page;

public class PageLoggerAccessQueryHandler(
    IMapper mapper
    ) : IRequestHandler<PageLoggerAccessQuery, Result>
{
    public async Task<Result> Handle(PageLoggerAccessQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // return Result.Success(new PaginationResult<>(results, total));
    }
}
