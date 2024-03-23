using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class ArticleUpdateCategoryEventHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryReop,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseMongoRepository<ArticleCollection> articleMongoResp
    ) : INotificationHandler<ArticleUpdateCategoryEvent>
{
    public async Task Handle(ArticleUpdateCategoryEvent notification, CancellationToken cancellationToken)
    {
        // 处理未存在初始化分类的情况
        if (notification.NewCategoryId == InitConst.InitCategoryId)
        {
            var initCategory = await categoryReop.Select.Where(c => c.Name == InitConst.InitCategoryName).FirstAsync(cancellationToken);
            if (initCategory == null)
            {
                await categoryReop.InsertAsync(new Category { CategoryId = InitConst.InitCategoryId, Name = InitConst.InitCategoryName }, cancellationToken);
            }
            else if (initCategory.CategoryId != InitConst.InitCategoryId)
            {
                initCategory.CategoryId = InitConst.InitCategoryId;
                await categoryReop.UpdateAsync(initCategory, cancellationToken);
            }
        }

        // 获取分类信息
        var category = await categoryReop.Select.Where(c => c.CategoryId == notification.NewCategoryId).FirstAsync(cancellationToken) ?? throw new Exception("分类不存在");

        var articles = await articleRepo.Select.Where(a => a.CategoryId == notification.CategoryId).ToListAsync(cancellationToken);

        foreach (var article in articles)
        {
            article.CategoryId = notification.NewCategoryId;
            article.Category = category;
        };
        await articleRepo.UpdateAsync(articles, cancellationToken);

        // 更新mongodb数据
        foreach (var article in articles)
        {
            var update = Builders<ArticleCollection>.Update
                .Set(nameof(ArticleCollection.Category), mapper.Map<ArticleCategoryBson>(article.Category));

            var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, article.ArticleId);
            var mongoUpdate = await articleMongoResp.UpdateOneAsync(update, filter, null, cancellationToken);
        }  
    }
}
