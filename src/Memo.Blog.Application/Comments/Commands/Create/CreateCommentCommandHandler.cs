using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Common.Interfaces.Services.Region;
using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Events.Articles;
using Memo.Blog.Domain.Events.Messages;

namespace Memo.Blog.Application.Comments.Commands.Create;

public class CreateCommentClientCommandHandler(
     // ILogger<CreateCommentClientCommandHandler> logger,
     IMapper mapper,
     ICurrentUserProvider currentUserProvider,
     IRegionSearchService searcher,
     IBaseDefaultRepository<Comment> commentRepo,
     IBaseDefaultRepository<Article> articleRepo,
     IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<CreateCommentClientCommand, Result>
{
    public async Task<Result> Handle(CreateCommentClientCommand request, CancellationToken cancellationToken)
    {
        var isArticleComment = false;
        long? articleAuthor = null;
        if (request.CommentType == BelongType.Article)
        {
            var article = await articleRepo.Select.Where(a => a.ArticleId == request.BelongId).FirstAsync(cancellationToken)
                ?? throw new ApplicationException("评论文章不存在");
            isArticleComment = true;
            articleAuthor = article.CreateUserId;
        }

        var comment = mapper.Map<Comment>(request);

        var visitorId = currentUserProvider.GetCurrentVisitor();
        if (visitorId <= 0) throw new ApplicationException("访客不存在");
        comment.VisitorId = visitorId;

        // 确认楼层
        // 如果是主楼
        var maxFloor = 0;
        if (!request.ParentId.HasValue)
        {
            maxFloor = await commentRepo.Select
                 .Where(c => !c.ParentId.HasValue)
                 .Where(c => c.CommentType == request.CommentType)
                 .Where(c => c.BelongId == request.BelongId)
                 .MaxAsync(c => c.Floor, cancellationToken);
        }
        else
        {
            // 如果是楼中楼
            maxFloor = await commentRepo.Select
                   .Where(c => c.ParentId.HasValue && c.ParentId == request.ParentId)
                   .MaxAsync(c => c.Floor, cancellationToken);

        }

        comment.Floor = maxFloor + 1;

        // 构建评论消息（用于推送通知）
        var commentMessage = new CommentMessageContent
        {
            CommentType = request.CommentType,
            Content = comment.Content,
        };

        // 如果是文章评论，则需要增加更新mongodb数据领域事件
        if (isArticleComment)
        {
            comment.AddDomainEvent(new UpdatedArticleCommentEvent(request.BelongId));
            commentMessage.BelongId = request.BelongId;
        }

        // 增加消息通知领域事件
        comment.AddDomainEvent(new CreateMessageEvent
        {
            UserId = visitorId,
            ToUsers = articleAuthor.HasValue ? [articleAuthor.Value] : [],
            ToRoles = [InitConst.InitAdminRoleId, InitConst.InitVisitorRoleId], // 按理说，只应该给管理员发以及作者，此处加上游客，演示用
            MessageType = MessageType.Comment,
            Content = commentMessage.ToJson()
        });

        var ip = currentUserProvider.GetClientIp();
        comment.Ip = ip;
        var region = searcher.Search(ip);
        comment.Region = region;
        comment.Showable = true;
        comment = await commentRepo.InsertAsync(comment, cancellationToken);

        comment.Visitor = await visitorRepo.Select
            .Where(c => c.VisitorId == comment.VisitorId)
            .FirstAsync(cancellationToken);

        var dto = mapper.Map<CommentClientResult>(comment);
        if (comment.ReplyId.HasValue)
        {
            var reply = await commentRepo.Select
                .Include(c => c.Visitor)
                .Where(c => c.CommentId == comment.ReplyId).FirstAsync(cancellationToken);
            if (reply != null)
                dto.Reply = mapper.Map<CommentReplyResult>(reply);
        }


        return comment.Id == 0 ? throw new ApplicationException("保存评论失败") : (Result)Result.Success(dto);
    }
}
