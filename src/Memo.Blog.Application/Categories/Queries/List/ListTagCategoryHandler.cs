using Memo.Blog.Application.Categories.Common;
using Memo.Blog.Application.Categories.Queries.Get;

namespace Memo.Blog.Application.Tags.Queries.Get;
public class ListCategoryQueryHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Category> _categoryResp
    ) : IRequestHandler<ListCategoryQuery, Result>
{
    public async Task<Result> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryResp.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .ToListAsync(cancellationToken) ?? [];

        // 获取初始化的分类
        var initCategory = categories.FirstOrDefault(c => c.CategoryId == 1);
        if (initCategory != null)
        {
            categories.Remove(initCategory);
            categories.Insert(0, initCategory);
        }

        return Result.Success(_mapper.Map<List<CategoryResult>>(categories));
    }
}
