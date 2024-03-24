namespace Memo.Blog.Application.Logger.Queries.Access.Get;

public class GetLoggerAccessQueryHandler(IMapper mapper) : IRequestHandler<GetLoggerAccessQuery, Result>
{
    public async Task<Result> Handle(GetLoggerAccessQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
       // return Result.Success(mapper.Map<>());
    }
}
