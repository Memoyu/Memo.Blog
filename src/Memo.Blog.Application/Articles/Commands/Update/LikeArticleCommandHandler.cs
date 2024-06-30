using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Events.Messages;

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

        var articleLike = new ArticleLike { ArticleId = article.ArticleId, VisitorId = visitorId };

        // 文章点赞事件
        articleLike.AddDomainEvent(new CreateMessageEvent
        {
            UserId = visitorId,
            ToUsers = [article.CreateUserId],
            ToRoles = [InitConst.InitAdminRoleId, InitConst.InitVisitorRoleId], // 按理说，只应该给管理员发以及作者，此处加上游客，演示用
            MessageType = MessageType.Like,
            Content = new LikeMessageContent
            {
                LikeType = BelongType.Article,
                BelongId = article.ArticleId,
            }.ToJson()
        });

        articleLike = await articleLickeRepo.InsertAsync(articleLike, cancellationToken)
            ?? throw new ApplicationException("点赞文章失败");

        return articleLike.Id > 0 ? Result.Success() : throw new ApplicationException("点赞文章失败");
    }
}

