using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Commands.Update;

public class UpdateArticleCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<Domain.Entities.Tag> tagRepo,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<UpdateArticleCommand, Result>
{
    public async Task<Result> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepo.Select.Where(a => a.ArticleId == request.ArticleId).FirstAsync(cancellationToken) ?? throw new ApplicationException("文章不存在");
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync(cancellationToken) ?? throw new ApplicationException("文章分类不存在");
        var tags = await tagRepo.Select.Where(t => request.Tags.Contains(t.TagId)).ToListAsync(cancellationToken);
        foreach (var tagId in request.Tags)
        {
            if (!tags.Any(t => t.TagId == tagId)) throw new ApplicationException($"{tagId}文章标签不存在");
        }
   
        var updateArticle = mapper.Map<Article>(request);
        updateArticle.Id = article.Id;
        // 不需要更新的字段
        updateArticle.Views = article.Views;
        // 判断是否需要更新状态
        updateArticle.Status = request.Status ?? article.Status;

        var row = await articleRepo.UpdateAsync(updateArticle, cancellationToken);
        if (row <= 0) throw new ApplicationException("更新文章失败");

        #region 文章关联标签管理

        var addArticleTags = new List<ArticleTag>();
        var currentArticleTags = await articleTagRepo.Select.Where(at => at.ArticleId == updateArticle.ArticleId).ToListAsync(cancellationToken);
        foreach (var tag in tags)
        {
            if (!currentArticleTags.Any(t => t.TagId == tag.TagId))
            {
                addArticleTags.Add(new ArticleTag { TagId = tag.TagId, ArticleId = request.ArticleId });
            }
            else
            {
                currentArticleTags.RemoveAll(t => t.TagId == tag.TagId);
            }
        }
        await articleTagRepo.InsertAsync(addArticleTags, cancellationToken);
        await articleTagRepo.DeleteAsync(currentArticleTags, cancellationToken);

        #endregion

        #region 更新MongoDB

        var articleCollection = mapper.Map<ArticleCollection>(updateArticle);
        var update = Builders<ArticleCollection>.Update
                 .Set(nameof(ArticleCollection.Category), mapper.Map<ArticleCategoryBson>(category))
                 .Set(nameof(ArticleCollection.Title), articleCollection.Title)
                 .Set(nameof(ArticleCollection.Description), articleCollection.Description)
                 .Set(nameof(ArticleCollection.Content), articleCollection.Content)
                 .Set(nameof(ArticleCollection.Banner), articleCollection.Banner)
                 .Set(nameof(ArticleCollection.Thumbnail), articleCollection.Thumbnail)
                 .Set(nameof(ArticleCollection.WordNumber), articleCollection.WordNumber)
                 .Set(nameof(ArticleCollection.ReadingTime), articleCollection.ReadingTime)
                 .Set(nameof(ArticleCollection.Status), articleCollection.Status)
                 .Set(nameof(ArticleCollection.Views), articleCollection.Views)
                 .Set(nameof(ArticleCollection.Likes), articleCollection.Likes)
                 .Set(nameof(ArticleCollection.IsTop), articleCollection.IsTop)
                 .Set(nameof(ArticleCollection.Commentable), articleCollection.Commentable)
                 .Set(nameof(ArticleCollection.Publicable), articleCollection.Publicable)
                 .Set(nameof(ArticleCollection.Tags), mapper.Map<List<ArticleTagBson>>(tags));

        var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, request.ArticleId);
        var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
        if (!mongoUpdate.IsAcknowledged) throw new Exception("更新mongodb失败"); 

        #endregion

        return Result.Success(updateArticle.ArticleId);
    }
}
