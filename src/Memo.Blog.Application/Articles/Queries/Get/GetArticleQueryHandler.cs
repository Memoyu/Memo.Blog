
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Categories.Common;
using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Tags.Common;
using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Articles.Queries.Get;

public class GetArticleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<TagArticle> tagArticleRepo,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseDefaultRepository<User> userRepo
    ) : IRequestHandler<GetArticleQuery, Result>
{
    public async Task<Result> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var article = await articleRepo.Select.Where(a => a.ArticleId == request.ArticleId).FirstAsync(cancellationToken);
        if (article is null) throw new ApplicationException("文章不存在");

        var category = await categoryRepo.Select
            .Where(c => c.CategoryId == article.CategoryId)
            .FirstAsync( cancellationToken);

        var tagArticles = await tagArticleRepo.Select
            .Where(ta => ta.ArticleId == request.ArticleId)
            .ToListAsync(cancellationToken);
        var tags = await tagRepo.Select
            .Where(t => tagArticles.Any(ta => ta.TagId == t.TagId))
            .ToListAsync(cancellationToken);

        var comments = await commentRepo.Select
            .Where(c => c.CommentType == Domain.Enums.CommentType.Article)
            .Where(c => c.BelongId == request.ArticleId)
            .ToListAsync(cancellationToken);

        var author = await userRepo.Select
            .Where(u => u.UserId == article.CreateUserId)
            .FirstAsync(cancellationToken);

        var result = mapper.Map<ArticleResult>(article);
        result.Category = mapper.Map<CategoryResult>(category);
        result.Tags = mapper.Map<List<TagResult>>(tags);
        result.Comments = mapper.Map<List<CommentResult>>(comments);
        result.Author = mapper.Map<UserResult>(author);

        return Result.Success(result);
    }
}
