using Memo.Blog.Application.Abouts.Common;
using Memo.Blog.Application.Abouts.Queries.Get;

namespace Memo.Blog.Application.Friends.Queries.Get;

public class GetAboutQueryHandler(IMapper mapper, IBaseDefaultRepository<About> aboutRepo) : IRequestHandler<GetAboutQuery, Result>
{
    public async Task<Result> Handle(GetAboutQuery request, CancellationToken cancellationToken)
    {
        var about = await aboutRepo.Select.FirstAsync(cancellationToken) ?? new();

        return Result.Success(mapper.Map<AboutResult>(about));
    }
}

public class GetAboutClientQueryHandler(IMapper mapper, IBaseDefaultRepository<About> aboutRepo) : IRequestHandler<GetAboutClientQuery, Result>
{
    public async Task<Result> Handle(GetAboutClientQuery request, CancellationToken cancellationToken)
    {
        var about = await aboutRepo.Select.FirstAsync(cancellationToken) ?? new();

        return Result.Success(mapper.Map<AboutResult>(about));
    }
}
