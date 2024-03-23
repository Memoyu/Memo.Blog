using Memo.Blog.Domain.Entities.Mongo;

namespace Memo.Blog.Application.Articles.Commands.Create;

public class CreateArticleCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleResp,
    IBaseDefaultRepository<TagArticle> tagArticleResp,
    IBaseMongoRepository<ArticleCollection> articleMongoResp,
    IBaseDefaultRepository<Tag> tagResp,
    IBaseDefaultRepository<Category> categoryResp
    ) : IRequestHandler<CreateArticleCommand, Result>
{
    public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryResp.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync();
        if (category is null) return Result.Failure("文章分类不存在");

        var tags = await tagResp.Select.Where(t => request.Tags.Contains(t.TagId)).ToListAsync();
        foreach (var tagId in request.Tags)
        {
            if (!tags.Any(t => t.TagId == tagId)) return Result.Failure($"{tagId}文章标签不存在");
        }

        var article = mapper.Map<Article>(request);
        article = await articleResp.InsertAsync(article, cancellationToken);
        if (article.Id == 0) return Result.Failure("保存文章失败");

        var tagArticles = request.Tags.Select(t => new TagArticle { ArticleId = article.ArticleId, TagId = t }).ToList();
        await tagArticleResp.InsertAsync(tagArticles);

        var articleCollection = mapper.Map<ArticleCollection>(article);
        var mongoInsert = await articleMongoResp.InsertOneAsync(articleCollection, null, cancellationToken);
        if (!mongoInsert) throw new Exception("写入mongodb失败");

        return Result.Success(article.ArticleId);
    }
}
