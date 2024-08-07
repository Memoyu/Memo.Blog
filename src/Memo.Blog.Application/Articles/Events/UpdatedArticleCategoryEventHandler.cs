﻿using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Events;

public class UpdatedArticleCategoryEventHandler(
    ISegmenterService segmenterService,
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
            var categorySegs = segmenterService.CutWithSplitForSearch(notification.Category.Name);
            var update = Builders<ArticleCollection>.Update
                .Set(nameof(ArticleCollection.Category), categorySegs.ToUtf8());

            var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, article.ArticleId);
            var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        }  
    }
}
