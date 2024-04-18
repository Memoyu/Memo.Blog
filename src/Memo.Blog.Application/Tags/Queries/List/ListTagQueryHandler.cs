using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Queries.List;
public class ListTagQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Tag> tagRepo
    ) : IRequestHandler<ListTagQuery, Result>
{
    public async Task<Result> Handle(ListTagQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .OrderByDescending(t => t.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        return Result.Success(mapper.Map<List<TagResult>>(tags));
    }
}
