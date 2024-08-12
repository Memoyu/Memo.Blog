
namespace Memo.Blog.Application.Configs.Queries.Get;

public class GetConfigQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<GetConfigQuery, Result>
{
    public async Task<Result> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
