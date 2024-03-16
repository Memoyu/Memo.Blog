
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Categories.Common;
using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Tags.Common;
using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Articles.Queries.Get;

public class PageArticleQueryHandler(
    IMapper mapper,
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
            .WhereIf(!string.IsNullOrWhiteSpace(request.Title), a => a.Title.Contains(request.Title))
            .WhereIf(request.CategoryId > 0, a => a.CategoryId == request.CategoryId)
            .WhereIf(request.TagIds.Any(), a => a.TagArticles.Any(ta => request.TagIds.Contains(ta.TagId)))
            .ToPageListAsync(request, out var total, cancellationToken);

        var result = mapper.Map<List<ArticlePageResult>>(articles);

        return Result.Success(result);
    }
}
