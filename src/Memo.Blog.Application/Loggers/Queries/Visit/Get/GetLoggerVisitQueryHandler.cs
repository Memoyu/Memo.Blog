namespace Memo.Blog.Application.Logger.Queries.Visit.Get;

public class GetLoggerVisitQueryHandler() : IRequestHandler<GetLoggerVisitQuery, Result>
{
    public Task<Result> Handle(GetLoggerVisitQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
       // return Result.Success(mapper.Map<>());
    }
}
