using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Events;

public class UpdatedArticleTagEventHandler(
    ISegmenterService segmenterService,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo,
    IBaseDefaultRepository<Tag> tagRepo
    ) : INotificationHandler<UpdatedArticleTagEvent>
{
    public async Task Handle(UpdatedArticleTagEvent notification, CancellationToken cancellationToken)
    {
        var articleTags = await articleTagRepo.Select.Where(at => at.TagId == notification.TagId).ToListAsync(cancellationToken);

        var articleIds = articleTags.Select(at => at.ArticleId).ToList();

        foreach (var articleId in articleIds)
        {
            // 更新mongodb文章详情
            var tagIds = await articleTagRepo.Select
                .Where(at => at.ArticleId == articleId)
                .ToListAsync(at => at.TagId, cancellationToken);

            // 不能使用Include, 会存在update的数据脏读问题
            var tags = await tagRepo.Select.Where(t => tagIds.Contains(t.TagId)).ToListAsync(t => t.Name, cancellationToken);
            // 所有标签组合，然后分词
            var tagSegs = segmenterService.CutWithSplitForSearch(string.Join(" ", tags));
            var update = MongoDB.Driver.Builders<ArticleCollection>.Update
                 .Set(nameof(ArticleCollection.Tags), tagSegs.ToUtf8());
            var filter = MongoDB.Driver.Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, articleId);
            var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        }
    }
}
