using Memo.Blog.Application.Categories.Common;

namespace Memo.Blog.Application.Categories.Queries.List;

public class ListCategoryQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Article> articleRepo
    ) : IRequestHandler<ListCategoryQuery, Result>
{
    public async Task<Result> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        // [未分类]排在最后一位
        var initCategory = categories.FirstOrDefault(c => c.CategoryId == 1);
        if (initCategory != null)
        {
            categories.Remove(initCategory);
            categories.Add(initCategory);
        }

        var articles = await articleRepo.Select.ToListAsync(a => new { a.ArticleId, a.CategoryId }, cancellationToken);
        var dtos = mapper.Map<List<CategoryWithArticleCountResult>>(categories);
        foreach (var category in dtos)
        {
            var total = articles.Where(a => a.CategoryId == category.CategoryId).Count();
            category.Articles = total;
        }

        return Result.Success(dtos);
    }
}

public class ListCategoryClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Article> articleRepo
    ) : IRequestHandler<ListCategoryClientQuery, Result>
{
    public async Task<Result> Handle(ListCategoryClientQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .OrderByDescending(c => c.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        // [未分类]排在最后一位
        var initCategory = categories.FirstOrDefault(c => c.CategoryId == 1);
        if (initCategory != null)
        {
            categories.Remove(initCategory);
            categories.Add(initCategory);
        }

        var articles = await articleRepo.Select.ToListAsync(a => new { a.ArticleId, a.CategoryId }, cancellationToken);
        var dtos = mapper.Map<List<CategoryWithArticleCountResult>>(categories);
        foreach (var category in dtos)
        {
            var total = articles.Where(a => a.CategoryId == category.CategoryId).Count();
            category.Articles = total;
        }

        return Result.Success(dtos);
    }
}
