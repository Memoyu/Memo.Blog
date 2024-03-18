using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Categories.Common;
using Memo.Blog.Application.Tags.Common;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Memo.Blog.Application.Articles.Commands.Update;

public class UpdateArticleCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleResp,
    IBaseDefaultRepository<TagArticle> tagArticleResp,
    IBaseMongoRepository<ArticleCollection> articleMongoResp,
    IBaseDefaultRepository<Domain.Entities.Tag> tagResp,
    IBaseDefaultRepository<Category> categoryResp
    ) : IRequestHandler<UpdateArticleCommand, Result>
{
    public async Task<Result> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var entity = await articleResp.Select.Where(a => a.ArticleId == request.ArticleId).FirstAsync();
        if (entity is null) return Result.Failure("文章不存在");

        var category = await categoryResp.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync();
        if (category is null) return Result.Failure("文章分类不存在");

        var tags = await tagResp.Select.Where(t => request.Tags.Contains(t.TagId)).ToListAsync();
        foreach (var tagId in request.Tags)
        {
            if (!tags.Any(t => t.TagId == tagId)) return Result.Failure($"{tagId}文章标签不存在");
        }
   
        var article = mapper.Map<Article>(request);
        article.Id = entity.Id;
        var row = await articleResp.UpdateAsync(article, cancellationToken);

        #region 标签管理

        var addTags = new List<TagArticle>();
        var currentTagArticles = await tagArticleResp.Select.Where(ta => ta.ArticleId == article.ArticleId).ToListAsync();
        foreach (var tag in tags)
        {
            if (!currentTagArticles.Any(t => t.TagId == tag.TagId))
            {
                addTags.Add(new TagArticle { TagId = tag.TagId, ArticleId = request.ArticleId });
            }
            else
            {
                currentTagArticles.RemoveAll(t => t.TagId == tag.TagId);
            }
        }
        await tagArticleResp.InsertAsync(addTags);
        await tagArticleResp.DeleteAsync(currentTagArticles);

        #endregion

        #region 更新MongoDB

        var articleCollection = mapper.Map<ArticleCollection>(article);
        var update = Builders<ArticleCollection>.Update
                 .Set(nameof(ArticleCollection.Category), category)
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
        var mongoUpdate = await articleMongoResp.UpdateOneAsync(update, filter, null, cancellationToken);
        if (!mongoUpdate.IsAcknowledged) throw new Exception("更新mongodb失败"); 

        #endregion

        var result = mapper.Map<ArticleResult>(article);
        result.Tags = mapper.Map<List<TagResult>>(tags);
        result.Category = mapper.Map<CategoryResult>(category);

        return Result.Success(result);
    }
}
