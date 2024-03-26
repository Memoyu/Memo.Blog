using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Queries.List;
public class ListTagQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Tag> tagResp
    ) : IRequestHandler<ListTagQuery, Result>
{
    public async Task<Result> Handle(ListTagQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagResp.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .ToListAsync(cancellationToken) ?? [];

        return Result.Success(mapper.Map<List<TagResult>>(tags));
    }
}
