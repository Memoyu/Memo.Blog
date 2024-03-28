using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Queries.Get;
public class GetTagQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Tag> tagRepo
    ) : IRequestHandler<GetTagQuery, Result>
{
    public async Task<Result> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepo.Select.Where(t => t.TagId == request.TagId).FirstAsync(cancellationToken);

        return tag is null ? throw new ApplicationException("标签不存在") : (Result)Result.Success(mapper.Map<TagResult>(tag));
    }
}
