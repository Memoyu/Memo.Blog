namespace Memo.Blog.Application.Logger.Queries.System.Get;

public class GetLoggerSystemQueryHandler(IMapper mapper) : IRequestHandler<GetLoggerSystemQuery, Result>
{
    public async Task<Result> Handle(GetLoggerSystemQuery request, CancellationToken cancellationToken)
    {
        throw new NotFiniteNumberException();
        // return Result.Success(mapper.Map<>());
    }
}
