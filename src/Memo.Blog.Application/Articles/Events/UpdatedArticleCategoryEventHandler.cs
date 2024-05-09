using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class UpdatedArticleCategoryEventHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo
    ) : INotificationHandler<UpdatedArticleCategoryEvent>
{
    public async Task Handle(UpdatedArticleCategoryEvent notification, CancellationToken cancellationToken)
    {
        // 获取分类相关文章信息
        var articles = await articleRepo.Select.Where(a => a.CategoryId == notification.Category.CategoryId).ToListAsync(cancellationToken);

        // 更新mongodb数据
        foreach (var article in articles)
        {
            var update = Builders<ArticleCollection>.Update
                .Set(nameof(ArticleCollection.Category), mapper.Map<ArticleCategoryBson>(notification.Category));

            var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, article.ArticleId);
            var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        }  
    }
}
