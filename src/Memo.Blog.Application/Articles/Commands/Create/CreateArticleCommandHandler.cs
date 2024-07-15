using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;

namespace Memo.Blog.Application.Articles.Commands.Create;

public class CreateArticleCommandHandler(
    IMapper mapper,
    IMarkdownService markdownService,
    ISegmenterService segmenterService,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<CreateArticleCommand, Result>
{
    public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync(cancellationToken);
        if (category is null) throw new ApplicationException("文章分类不存在");

        var tags = await tagRepo.Select.Where(t => request.Tags.Contains(t.TagId)).ToListAsync(cancellationToken);
        foreach (var tagId in request.Tags)
        {
            if (!tags.Any(t => t.TagId == tagId)) throw new ApplicationException($"{tagId}文章标签不存在");
        }

        var article = mapper.Map<Article>(request);

        article = await articleRepo.InsertAsync(article, cancellationToken);
        if (article.Id == 0) throw new ApplicationException("保存文章失败");

        var tagArticles = request.Tags.Select(t => new ArticleTag { ArticleId = article.ArticleId, TagId = t }).ToList();
        await articleTagRepo.InsertAsync(tagArticles, cancellationToken);

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

        return Result.Success(article.ArticleId);
    }
}
