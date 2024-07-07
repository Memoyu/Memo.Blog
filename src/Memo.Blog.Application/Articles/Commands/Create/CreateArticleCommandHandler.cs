using Memo.Blog.Domain.Entities.Mongo;

namespace Memo.Blog.Application.Articles.Commands.Create;

public class CreateArticleCommandHandler(
    IMapper mapper,
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

        var articleCollection = mapper.Map<ArticleCollection>(article);
        var mongoInsert = await articleMongoRepo.InsertOneAsync(articleCollection, null, cancellationToken);
        if (!mongoInsert) throw new Exception("写入mongodb失败");

        return Result.Success(article.ArticleId);
    }
}
