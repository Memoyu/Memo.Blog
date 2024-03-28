using Memo.Blog.Domain.Events.Articles;
using Memo.Blog.Domain.Events.Categories;

namespace Memo.Blog.Application.Categories.Events;

public class DeletedCategoryEventHadler(
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Article> articleRepo
    ) : INotificationHandler<DeletedCategoryEvent>
{
    public async Task Handle(DeletedCategoryEvent notification, CancellationToken cancellationToken)
    {
        // 处理未存在初始化分类的情况
        var initCategory = await categoryRepo.Select.Where(c => c.Name == InitConst.InitCategoryName).FirstAsync(cancellationToken);
        if (initCategory == null)
        {
            initCategory = await categoryRepo.InsertAsync(new Category { CategoryId = InitConst.InitCategoryId, Name = InitConst.InitCategoryName }, cancellationToken);
        }
        else if (initCategory.CategoryId != InitConst.InitCategoryId)
        {
            initCategory.CategoryId = InitConst.InitCategoryId;
            await categoryRepo.UpdateAsync(initCategory, cancellationToken);
        }

        // 获取分类相关文章信息
        var articles = await articleRepo.Select.Where(a => a.CategoryId == notification.CategoryId).ToListAsync(cancellationToken);

        foreach (var article in articles)
        {
            article.CategoryId = initCategory.CategoryId;
            article.Category = initCategory;
            article.AddDomainEvent(new UpdatedArticleCategoryEvent(initCategory));
        }

       var affrows = await articleRepo.UpdateAsync(articles, cancellationToken);
    }
}
