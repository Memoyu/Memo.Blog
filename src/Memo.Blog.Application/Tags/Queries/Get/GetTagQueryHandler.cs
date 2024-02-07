using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Queries.Get;
public class GetTagQueryHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Tag> _tagResp
    ) : IRequestHandler<GetTagQuery, Result>
{
    public async Task<Result> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await _tagResp.Select.Where(t => t.TagId == request.TagId).FirstAsync(cancellationToken);
        if (tag is null) return Result.Failure("标签不存在");

        return Result.Success(_mapper.Map<TagResult>(tag));
    }
}
