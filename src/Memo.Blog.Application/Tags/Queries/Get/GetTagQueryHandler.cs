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

        return tag is null ? throw new ApplicationException("标签不存在") : (Result)Result.Success(_mapper.Map<TagResult>(tag));
    }
}
