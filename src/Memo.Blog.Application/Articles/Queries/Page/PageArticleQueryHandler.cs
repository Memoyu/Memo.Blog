
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Articles.Queries.Page;

public class PageArticleQueryHandler(
    IMapper mapper,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Category> categoryRepo,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<TagArticle> tagArticleRepo,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseDefaultRepository<User> userRepo
    ) : IRequestHandler<PageArticleQuery, Result>
{
    public async Task<Result> Handle(PageArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = await articleRepo.Select
            .Include(a => a.Category)
            .IncludeMany(a => a.TagArticles, then => then.Include(t => t.Tag))
            .Where(a => a.CreateUserId == currentUserProvider.GetCurrentUser().Id)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Title), a => a.Title.Contains(request.Title!))
            .WhereIf(request.CategoryId > 0, a => a.CategoryId == request.CategoryId)
            .WhereIf(request.TagIds != null && request.TagIds.Any(), a => a.TagArticles.Any(ta => request.TagIds!.Contains(ta.TagId)))
            .WhereIf(request.Status.HasValue, a=> a.Status == request.Status!.Value)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<ArticlePageResult>>(articles);

        return Result.Success(new PaginationResult<ArticlePageResult>(results, total));
    }
}
