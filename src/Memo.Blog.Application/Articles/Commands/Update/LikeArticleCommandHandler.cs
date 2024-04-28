using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Articles.Commands.Update;

public class LikeArticleCommandHandler(
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Article> articleRepo
    ) : IRequestHandler<LikeArticleCommand, Result>
{
    public async Task<Result> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
    {
        var visitorId = currentUserProvider.GetCurrentVisitor();
        var article = await articleRepo.Select.Where(t => t.ArticleId == request.ArticleId).FirstAsync(cancellationToken)
            ?? throw new ApplicationException("文章不存在");

        article.Likes += 1;
        var affrows = await articleRepo.UpdateAsync(article, cancellationToken);
        return affrows > 0 ? Result.Success() : throw new ApplicationException("点赞文章失败");
    }
}

