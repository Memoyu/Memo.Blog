using EasyCaching.Core;
using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Events;

public class UpdatedArticleViewsEventHandler(
    IEasyCachingProvider ecProvider,
    IBaseDefaultRepository<Article> articleRepo
    ) : INotificationHandler<UpdatedArticleViewsEvent>
{
    public async Task Handle(UpdatedArticleViewsEvent notification, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyConst.ArticleView(notification.ArticleId, notification.VisitorId);
        var view = await ecProvider.GetAsync<long>(cacheKey, cancellationToken);
        if (view != null && view.HasValue) return;

        // 更新文章阅读数，只更新数据库，mongodb可不更新
        var article = await articleRepo.Select
            .Where(a => a.ArticleId == notification.ArticleId)
            .FirstAsync(cancellationToken);

        if (article == null) return;

        article.Views += 1;

        await articleRepo.UpdateAsync(article, cancellationToken);
        await ecProvider.SetAsync(cacheKey, notification.ArticleId, TimeSpan.FromMinutes(5), cancellationToken); // 五分钟内同一个访客，只算一次浏览次数
    }
}
