using MediatR;
using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Common.Interfaces.Services.Region;
using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Events.Articles;
using Memo.Blog.Domain.Events.Messages;

namespace Memo.Blog.Application.Comments.Commands.Create;

public class CreateCommentCommandHandler(
    IMapper mapper,
    IMediator mediator,
    ICurrentUserProvider currentUserProvider) : IRequestHandler<CreateCommentCommand, Result>
{
    public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        // 管理端请求时传入访客Id
        var userId = currentUserProvider.GetCurrentUser().Id;
        var command = mapper.Map<CommonCreateCommentCommand>(request);
        command.ExcludeUsers = new List<long> { userId };
        var result = await mediator.Send(command, cancellationToken);
        return Result.Success(result.CommentId);
    }
}

public class CreateCommentClientCommandHandler(
    IMapper mapper,
    IMediator mediator,
    ICurrentUserProvider currentUserProvider
    ) : IRequestHandler<CreateCommentClientCommand, Result>
{
    public async Task<Result> Handle(CreateCommentClientCommand request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CommonCreateCommentCommand>(request);

        // 客户端从header获取访客Id
        var visitorId = currentUserProvider.GetCurrentVisitor();
        if (visitorId <= 0) throw new ApplicationException("访客不存在");
        command.VisitorId = visitorId;

        var result = await mediator.Send(command, cancellationToken);
        return Result.Success(result);
    }
}

public class CommonCreateCommentCommandHandler(
     IMapper mapper,
     ICurrentUserProvider currentUserProvider,
     IRegionSearchService searcher,
     IBaseDefaultRepository<Comment> commentRepo,
     IBaseDefaultRepository<Article> articleRepo,
     IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<CommonCreateCommentCommand, CommentClientResult>
{
    public async Task<CommentClientResult> Handle(CommonCreateCommentCommand request, CancellationToken cancellationToken)
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

        // 补全访客信息
        comment.Visitor = await visitorRepo.Select
            .Where(c => c.VisitorId == comment.VisitorId)
            .FirstAsync(cancellationToken);

        // 增加消息通知领域事件
        comment.AddDomainEvent(new CreateMessageEvent
        {
            UserId = comment.VisitorId,
            ToUsers = articleAuthor.HasValue ? [articleAuthor.Value] : [],
            ToRoles = [InitConst.InitAdminRoleId, InitConst.InitVisitorRoleId], // 按理说，只应该给管理员发以及作者，此处加上游客，演示用
            ExcludeUsers = request.ExcludeUsers,
            MessageType = MessageType.Comment,
            Content = commentMessage.ToJson()
        });

        // 给回复人发送一个邮件通知
        Comment? reply = null;
        if (comment.ReplyId.HasValue)
        {
            reply = await commentRepo.Select
                    .Include(c => c.Visitor)
                    .Where(c => c.CommentId == comment.ReplyId).FirstAsync(cancellationToken);

            if (reply.Visitor != null)
            {
                // 构建邮箱消息通知模型，并推送邮件
                comment.AddDomainEvent(new MessageReplyEmailEvent
                {
                    Source = reply,
                    Reply = comment
                });
            }
        }

        var ip = currentUserProvider.GetClientIp();
        comment.Ip = ip;
        var region = searcher.Search(ip);
        comment.Region = region;
        comment.Showable = true;
        comment = await commentRepo.InsertAsync(comment, cancellationToken);

        var dto = mapper.Map<CommentClientResult>(comment);
        if (reply != null)
            dto.Reply = mapper.Map<CommentReplyResult>(reply);

        return comment.Id == 0 ? throw new ApplicationException("保存评论失败") : dto;
    }
}

