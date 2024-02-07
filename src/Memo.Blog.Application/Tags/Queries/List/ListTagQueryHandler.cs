using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Queries.Get;
public class ListTagQueryHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Tag> _tagResp
    ) : IRequestHandler<ListTagQuery, Result>
{
    public async Task<Result> Handle(ListTagQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagResp.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .ToListAsync(cancellationToken) ?? [];

        return Result.Success(_mapper.Map<List<TagResult>>(tags));
    }
}
