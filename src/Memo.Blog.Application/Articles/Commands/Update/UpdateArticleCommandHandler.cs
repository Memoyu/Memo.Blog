using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Commands.Update;

public class UpdateArticleCommandHandler(
    IMapper mapper,
    IMarkdownService markdownService,
    ISegmenterService segmenterService,
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

        var tagsChange = false;
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

        if (addArticleTags.Count != 0 || currentArticleTags.Count != 0) tagsChange = true;

        #endregion

        #region 更新MongoDB


        var articleCol = await articleMongoRepo.FindOneAsync(article.ArticleId, false);
        if (articleCol == null)
        {  
            // 文章内容处理
            var text = markdownService.RemoveTag(request.Content);
            var contentSegs = segmenterService.CutWithSplitForSearch(string.Join(" ", text));

            // 所有标签组合，然后分词
            var tagNames = tags.Select(t => t.Name).ToList();
            var tagSegs = segmenterService.CutWithSplitForSearch(string.Join(" ", tagNames));

            var articleCollection = new ArticleCollection
            {
                ArticleId = article.ArticleId,
                Category = category.Name,
                Tags = tagSegs,
                Title = segmenterService.CutWithSplitForSearch(string.Join(" ", article.Title)),
                Description = segmenterService.CutWithSplitForSearch(string.Join(" ", article.Description)),
                Content = contentSegs,
                Status = article.Status,
                CreateTime = article.CreateTime,
            };
            var mongoInsert = await articleMongoRepo.InsertOneAsync(articleCollection, null, cancellationToken);
            if (!mongoInsert) throw new Exception("写入mongodb失败");
        }
        else
        {
            // 更新文章内容、标签、分类等
            var updateProps = new List<UpdateDefinition<ArticleCollection>>();
            if (article.CategoryId != request.CategoryId)
                updateProps.Add(Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Category), category.Name));

            if (!article.Title.Equals(request.Title))
                updateProps.Add(Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Title), segmenterService.CutWithSplitForSearch(string.Join(" ", request.Title))));

            if (!article.Description.Equals(request.Description))
                updateProps.Add(Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Description), segmenterService.CutWithSplitForSearch(string.Join(" ", request.Description))));

            if (!article.Content.Equals(request.Content))
            {
                var text = markdownService.RemoveTag(request.Content);
                var contentSegs = segmenterService.CutWithSplitForSearch(string.Join(" ", text));
                updateProps.Add(Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Content), contentSegs));
            }

            if (tagsChange)
            {
                // 所有标签组合，然后分词
                var tagNames = tags.Select(t => t.Name).ToList();
                var tagSegs = segmenterService.CutWithSplitForSearch(string.Join(" ", tagNames));
                updateProps.Add(Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Category), tagSegs));
            }

            if (article.Status != request.Status)
            { 
                 updateProps.Add(Builders<ArticleCollection>.Update.Set(nameof(ArticleCollection.Status), request.Status));
            }

            if (updateProps.Count != 0)
            {
                var update = Builders<ArticleCollection>.Update.Combine(updateProps);
                var filter = Builders<ArticleCollection>.Filter.Eq(b => b.ArticleId, article.ArticleId);
                var mongoUpdate = await articleMongoRepo.UpdateOneAsync(update, filter, null, cancellationToken);
                if (!mongoUpdate.IsAcknowledged) throw new Exception("更新mongodb失败");
            }
        }

        #endregion

        return Result.Success(updateArticle.ArticleId);
    }
}
