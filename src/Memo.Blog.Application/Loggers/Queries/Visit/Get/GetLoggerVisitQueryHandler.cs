namespace Memo.Blog.Application.Logger.Queries.Visit.Get;

public class GetLoggerVisitQueryHandler(IMapper mapper) : IRequestHandler<GetLoggerVisitQuery, Result>
{
    public async Task<Result> Handle(GetLoggerVisitQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
       // return Result.Success(mapper.Map<>());
    }
}
