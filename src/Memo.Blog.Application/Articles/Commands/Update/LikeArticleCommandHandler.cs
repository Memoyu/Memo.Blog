using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Articles.Commands.Update;

public class LikeArticleCommandHandler(
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<ArticleLike> articleLickeRepo
    ) : IRequestHandler<LikeArticleCommand, Result>
{
    public async Task<Result> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
    {
        var visitorId = currentUserProvider.GetCurrentVisitor();
        if (visitorId <= 0) throw new ApplicationException("点赞文章失败");

        var article = await articleRepo.Select.Where(t => t.ArticleId == request.ArticleId).FirstAsync(cancellationToken)
            ?? throw new ApplicationException("文章不存在");

        // 已经点过赞
        var hasLike = await articleLickeRepo.Select.AnyAsync(al => al.VisitorId == visitorId && al.ArticleId == article.ArticleId, cancellationToken);
        if (hasLike) return Result.Success();

        var entity = await articleLickeRepo.InsertAsync(new ArticleLike { ArticleId = article.ArticleId, VisitorId = visitorId }, cancellationToken)
            ?? throw new ApplicationException("点赞文章失败");

        return entity.Id > 0 ? Result.Success() : throw new ApplicationException("点赞文章失败");
    }
}

