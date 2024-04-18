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
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        //// 获取初始化的分类
        //var initCategory = categories.FirstOrDefault(c => c.CategoryId == 1);
        //if (initCategory != null)
        //{
        //    categories.Remove(initCategory);
        //    categories.Insert(0, initCategory);
        //}

        return Result.Success(mapper.Map<List<CategoryResult>>(categories));
    }
}

public class ClientListCategoryQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Article> articleRepo
    ) : IRequestHandler<ClientListCategoryQuery, Result>
{
    public async Task<Result> Handle(ClientListCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        // 未分类排在第一位
        var initCategory = categories.FirstOrDefault(c => c.CategoryId == 1);
        if (initCategory != null)
        {
            categories.Remove(initCategory);
            categories.Insert(0, initCategory);
        }

        var articles = await articleRepo.Select.ToListAsync(a => new { a.ArticleId, a.CategoryId }, cancellationToken);
        var dtos = mapper.Map<List<CategoryWithArticleCountResult>>(categories);
        foreach (var category in dtos)
        {
            var total = articles.Where(a => a.CategoryId == category.CategoryId).Count();
            category.Articles = total;
        }

        // 添加全部
        dtos?.Insert(0, new CategoryWithArticleCountResult { CategoryId = 0, Name = "全部", Articles = articles.Count });

        return Result.Success(dtos);
    }
}
