using System.Threading;
using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageUserEmailEventHandler(
    IOptionsMonitor<AppSettings> appSettings,
    IOptionsMonitor<AuthorizationSettings> authOptions,
    ILogger<MessageUserEmailEventHandler> logger,
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<Visitor> visitorRepo,
    IBaseDefaultRepository<Article> articleRepo,
    IMailService mailService) : INotificationHandler<MessageUserEmailEvent>
{
    public async Task Handle(MessageUserEmailEvent notification, CancellationToken cancellationToken)
    {
        // 未启用邮件推送提醒，则直接退出
        var mailOptions = authOptions.CurrentValue?.Mail ?? new();
        if (!mailOptions.Enable) return;

        if (notification.ToUsers == null || notification.ToUsers.Count < 1)
        {
            logger.LogError("邮件推送时，接收方Ids为空，取消推送");
            return;
        }



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
                var username = fromUser == null ? string.Empty : $" {fromUser.Nickname} ";
                subject = "嗒嗒！用户" + username + "发来的消息";
                body = @$"
访客：{username} 发来的消息：
<br/>{userMsg.Content}
<br/>
<br/>------------------------------------------
<br/>这是系统自动通知邮件，不需要回复哈。";
                break;
            case MessageType.Comment:
                var commentMsg = notification.Content.ToDesJson<CommentMessageContent>() ?? throw new ApplicationException("邮件推送中消息内容为空");
                var (cKey, cLink) = await GetContentKeyAsync(commentMsg.CommentType, commentMsg.BelongId, cancellationToken);

                var commentVisitor = await visitorRepo.Select.Where(u => u.VisitorId == notification.FromUser).FirstAsync(cancellationToken);
                subject = "嘀嘀！" + cKey + "有新评论啦！";
                body = @$"
访客：{(commentVisitor == null ? string.Empty : $" {commentVisitor.Nickname} ")} 发来评论：
<br/>{commentMsg.Content}
<br/>
<br/>------------------------------------------
<br/>访问链接：<a href='{cLink}'>{cLink}</a>
<br/>这是系统自动通知邮件，不需要回复哈。";
                break;
            case MessageType.Like:
                var likeMsg = notification.Content.ToDesJson<LikeMessageContent>() ?? throw new ApplicationException("邮件推送中消息内容为空");
                var (lKey, lLink) = await GetContentKeyAsync(likeMsg.LikeType, likeMsg.BelongId, cancellationToken);

                var likeVisitor = await visitorRepo.Select.Where(u => u.VisitorId == notification.FromUser).FirstAsync(cancellationToken);
                subject = "嘻嘻！" + lKey + "收到点赞咯！";
                body = @$"
访客：{(likeVisitor == null ? string.Empty : $" {likeVisitor.Nickname} ")} 点了个赞！
<br/>
<br/>------------------------------------------
<br/>访问链接：<a href='{lLink}'>{lLink}</a>
<br/>这是系统自动通知邮件，不需要回复哈。";
                break;
            default:
                throw new NotImplementedException("未实现该类型消息的邮件通知");
        }

        // 异步发送邮件，不需要等待
        _ = mailService.SendAsync(new MailMsg
        {
            Subject = subject,
            Body = body,
            Tos = toEmails,
        });

    }

    private async Task<(string key, string link)> GetContentKeyAsync(BelongType type, long belongId, CancellationToken cancellationToken)
    {
        var key = string.Empty;
        var link = string.Empty;
        var clientDomain = appSettings.CurrentValue?.ClientDomain;

        switch (type)
        {
            case BelongType.Article:
                var article = await articleRepo.Select.Where(a => a.ArticleId == belongId).FirstAsync(cancellationToken);
                key = $"文章：[{article.Title}]";
                link = clientDomain + AppConst.ClientArticleDetail + article.ArticleId;
                break;
            case BelongType.Moment:
                key = "[动态]";
                link = clientDomain + AppConst.ClientMomentList;
                break;
            case BelongType.About:
                key = "[关于我]";
                break;
        }

        return (key, link);
    }
}
