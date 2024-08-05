using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qiniu.Util;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageEmailEventHandler(
    IOptionsMonitor<AppSettings> appSettings,
    IOptionsMonitor<AuthorizationSettings> authOptions,
    ILogger<MessageEmailEventHandler> logger,
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<Visitor> visitorRepo,
    IBaseDefaultRepository<Article> articleRepo,
    IMailService mailService) : INotificationHandler<MessageEmailEvent>
{
    public async Task Handle(MessageEmailEvent notification, CancellationToken cancellationToken)
    {
        // 未启用邮件推送提醒，则直接退出
        var mailOptions = authOptions.CurrentValue?.Mail ?? new();
        if (!mailOptions.Enable) return;

        if (notification.ToUsers == null || notification.ToUsers.Count < 1)
        {
            logger.LogError("邮件推送时，接收方Ids为空，取消推送");
            return;
        }

        var clientDomain = appSettings.CurrentValue?.ClientDomain;

        // 通过邮件发送给指定用户

        // 获取用户邮箱
        var toEmails = await userRepo.Select
            .Where(u => notification.ToUsers.Contains(u.UserId) && !string.IsNullOrWhiteSpace(u.Email))
            .ToListAsync(u => u.Email, cancellationToken);
        toEmails = toEmails.Distinct().ToList();

        // 没有邮箱，就不发了
        if (toEmails.Count < 1) return;

        // 解析消息内容，构建邮件标题、内容
        var subject = string.Empty;
        var body = string.Empty;
        switch (notification.Type)
        {
            case MessageType.User:
                var userMsg = notification.Content.ToDesJson<UserMessageContent>() ?? throw new ApplicationException("邮件推送中消息内容为空");
                var fromUser = await userRepo.Select.Where(u => u.UserId == notification.FromUser).FirstAsync(cancellationToken);
                subject = "用户" + (fromUser == null ? string.Empty : $" {fromUser.Nickname} ") + "发来的消息";
                body = $"收到了{subject}<br/><br/>消息内容如下：<br/>{userMsg.Content}";
                break;
            case MessageType.Comment:
                var commentMsg = notification.Content.ToDesJson<CommentMessageContent>() ?? throw new ApplicationException("邮件推送中消息内容为空");
                var commentTitle = string.Empty;
                var commentLink = string.Empty;
                switch (commentMsg.CommentType)
                {
                    case BelongType.Article:
                        var article = await articleRepo.Select.Where(a => a.ArticleId == commentMsg.BelongId).FirstAsync(cancellationToken);
                        commentTitle = $"评论了文章：{article.Title}";
                        commentLink = clientDomain + AppConst.ClientArticleDetail + article.ArticleId;
                        break;
                    case BelongType.Moment:
                        commentTitle = "评论了动态";
                        commentLink = clientDomain + AppConst.ClientMomentList;
                        break;
                    case BelongType.About:
                        commentTitle = "评论了关于我";
                        break;
                }

                var commentVisitor = await visitorRepo.Select.Where(u => u.VisitorId == notification.FromUser).FirstAsync(cancellationToken);
                subject = "访客" + (commentVisitor == null ? string.Empty : $" {commentVisitor.Nickname} ") + commentTitle;
                body = $"{subject}<br/><br/>访问链接：<a href='{commentLink}'>{commentLink}</a><br/><br/>评论内容如下：<br/>{commentMsg.Content}";
                break;
            case MessageType.Like:
                var likeMsg = notification.Content.ToDesJson<LikeMessageContent>() ?? throw new ApplicationException("邮件推送中消息内容为空");
                var likeTitle = string.Empty;
                var likeLink = string.Empty;
                switch (likeMsg.LikeType)
                {
                    case BelongType.Article:
                        var article = await articleRepo.Select.Where(a => a.ArticleId == likeMsg.BelongId).FirstAsync(cancellationToken);
                        likeTitle = $"点赞了文章：{article.Title}";
                        likeLink = clientDomain + AppConst.ClientArticleDetail + article.ArticleId;
                        break;
                    case BelongType.Moment:
                        likeTitle = "点赞了动态";
                        likeLink = clientDomain + AppConst.ClientMomentList;
                        break;
                    case BelongType.About:
                        likeTitle = "点赞了关于我";
                        break;
                }

                var likeVisitor = await visitorRepo.Select.Where(u => u.VisitorId == notification.FromUser).FirstAsync(cancellationToken);
                subject = "访客" + (likeVisitor == null ? string.Empty : $" {likeVisitor.Nickname} ") + likeTitle;
                body = $"{subject}<br/><br/>访问链接：<a href='{likeLink}'>{likeLink}</a>";
                break;
            default:
                throw new NotImplementedException("未实现该类型消息的邮件通知");
        }

        // 异步发送邮件，不需要等待
        _ = Task.Run(() =>
            mailService.Send(new MailMsg
            {
                Subject = subject,
                Body = body,
                Tos = toEmails,
            }), cancellationToken);

    }
}
