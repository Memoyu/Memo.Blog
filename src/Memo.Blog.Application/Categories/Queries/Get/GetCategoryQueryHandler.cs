using Memo.Blog.Application.Categories.Common;

namespace Memo.Blog.Application.Categories.Queries.Get;

public class GetCategoryQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<GetCategoryQuery, Result>
{
    public async Task<Result> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(t => t.CategoryId == request.CategoryId).FirstAsync(cancellationToken);
        return category is null ? throw new ApplicationException("分类不存在") : (Result)Result.Success(mapper.Map<CategoryResult>(category));
    }
}
