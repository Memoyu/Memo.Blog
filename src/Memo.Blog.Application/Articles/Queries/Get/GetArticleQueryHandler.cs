
using System.ComponentModel;
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Categories.Common;
using Memo.Blog.Application.Security;
using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Articles.Queries.Get;

public class GetArticleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo
    ) : IRequestHandler<GetArticleQuery, Result>
{
    public async Task<Result> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var article = await articleRepo.Select
            .Where(a => a.ArticleId == request.ArticleId)
            .FirstAsync(cancellationToken) ?? throw new ApplicationException("文章不存在");

        var category = await categoryRepo.Select
            .Where(c => c.CategoryId == article.CategoryId)
            .FirstAsync(cancellationToken);

        var articleTags = await articleTagRepo.Select
            .Where(at => at.ArticleId == request.ArticleId)
            .ToListAsync(cancellationToken);
        var tags = await tagRepo.Select
            .Where(t => articleTags.Any(at => at.TagId == t.TagId))
            .ToListAsync(cancellationToken);

        var result = mapper.Map<ArticleDetailResult>(article);
        result.Category = mapper.Map<CategoryResult>(category);
        result.Tags = mapper.Map<List<TagResult>>(tags);

        return Result.Success(result);
    }
}

public class GetArticleClientQueryHandler(
    IMapper mapper,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo,
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<ArticleLike> articleLikeRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<GetArticleClientQuery, Result>
{
    public async Task<Result> Handle(GetArticleClientQuery request, CancellationToken cancellationToken)
    {
        var article = await articleRepo.Select
            .Where(a => a.ArticleId == request.ArticleId)
            .Where(a => a.Status == Domain.Enums.ArticleStatus.Published)
            .FirstAsync(cancellationToken) ?? throw new ApplicationException("文章不存在或已删除");

        var category = await categoryRepo.Select
            .Where(c => c.CategoryId == article.CategoryId)
            .FirstAsync(cancellationToken);

        var articleTags = await articleTagRepo.Select
            .Where(at => at.ArticleId == request.ArticleId)
            .ToListAsync(cancellationToken);
        var tags = await tagRepo.Select
            .Where(t => articleTags.Any(at => at.TagId == t.TagId))
            .ToListAsync(cancellationToken);

        var author = await userRepo.Select
            .Where(u => u.UserId == article.CreateUserId)
            .FirstAsync(cancellationToken);

        var likes = await articleLikeRepo.Select
            .Where(al => al.ArticleId == article.ArticleId)
            .ToListAsync(al => al.VisitorId, cancellationToken);


        var visitorId = currentUserProvider.GetCurrentVisitor();


        var comments = (int)await commentRepo.Select
             .Where(c => c.BelongId == article.ArticleId)
             .CountAsync(cancellationToken);

        var result = mapper.Map<ArticleDetailClientResult>(article);
        result.Category = mapper.Map<CategoryResult>(category);
        result.Tags = mapper.Map<List<TagResult>>(tags);
        result.Author = mapper.Map<ArticleAuthorResult>(author);

        result.Likes = likes.Count;
        if (visitorId > 0)
            result.IsLike = likes.Any(v => v == visitorId);

        result.Comments = comments;

        return Result.Success(result);
    }
}
