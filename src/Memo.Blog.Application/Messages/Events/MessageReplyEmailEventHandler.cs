using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.Extensions.Options;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageReplyEmailEventHandler(
    IOptionsMonitor<AppSettings> appSettings,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseDefaultRepository<Visitor> visitorRepo,
    IMailService mailService) : INotificationHandler<MessageReplyEmailEvent>
{
    public async Task Handle(MessageReplyEmailEvent notification, CancellationToken cancellationToken)
    {
        var toEmail = notification.Reply.Email;
        if (string.IsNullOrWhiteSpace(toEmail)) return;

        var clientDomain = appSettings.CurrentValue?.ClientDomain;
        var visitor = await visitorRepo.Select.Where(u => u.VisitorId == notification.VisitorId).FirstAsync(cancellationToken);

        var subject = string.Empty;
        var body = string.Empty;
        var commentMsg = notification.Content.ToDesJson<CommentMessageContent>() ?? throw new ApplicationException("邮件推送中消息内容为空");
        var commentTitle = string.Empty;
        var commentLink = string.Empty;
        switch (commentMsg.CommentType)
        {
            case BelongType.Article:
                var article = await articleRepo.Select.Where(a => a.ArticleId == commentMsg.BelongId).FirstAsync(cancellationToken);
                commentTitle = $"[文章：{article.Title}]";
                commentLink = clientDomain + AppConst.ClientArticleDetail + article.ArticleId;
                break;
            case BelongType.Moment:
                commentTitle = "[动态]";
                commentLink = clientDomain + AppConst.ClientMomentList;
                break;
            case BelongType.About:
                commentTitle = "[关于我]";
                break;
        }

        subject =  $"{visitor.Nickname} 回复你了在{commentTitle}发起的评论";
        body = $"{notification.Reply.Nickname} 你好！<br/><br/>{visitor.Nickname}回复了你的评论：<br/>{commentMsg.Content}<br/><br/>访问链接：<a href='{commentLink}'>{commentLink}</a>";

        // 异步发送邮件，不需要等待
        _ = Task.Run(() =>
            mailService.Send(new MailMsg
            {
                Subject = subject,
                Body = body,
                Tos = new List<string> { toEmail },
            }), cancellationToken);
    }
}
