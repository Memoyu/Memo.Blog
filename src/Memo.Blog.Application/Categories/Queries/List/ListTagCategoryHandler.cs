using Memo.Blog.Application.Categories.Common;

namespace Memo.Blog.Application.Categories.Queries.List;

public class ListCategoryQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<ListCategoryQuery, Result>
{
    public async Task<Result> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .ToListAsync(cancellationToken) ?? [];

        // 获取初始化的分类
        var initCategory = categories.FirstOrDefault(c => c.CategoryId == 1);
        if (initCategory != null)
        {
            categories.Remove(initCategory);
            categories.Insert(0, initCategory);
        }

        return Result.Success(mapper.Map<List<CategoryResult>>(categories));
    }
}
