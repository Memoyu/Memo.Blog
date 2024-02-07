using Memo.Blog.Application.Categories.Common;

namespace Memo.Blog.Application.Categories.Queries.Get;

public class GetCategoryQueryHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Category> _categoryResp
    ) : IRequestHandler<GetCategoryQuery, Result>
{
    public async Task<Result> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var tag = await _categoryResp.Select.Where(t => t.CategoryId == request.CategoryId).FirstAsync(cancellationToken);
        if (tag is null) return Result.Failure("分类不存在");

        return Result.Success(_mapper.Map<CategoryResult>(tag));
    }
}
