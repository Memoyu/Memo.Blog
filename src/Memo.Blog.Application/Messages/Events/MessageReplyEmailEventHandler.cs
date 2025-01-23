using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.Extensions.Options;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageReplyEmailEventHandler(
    IOptionsMonitor<AppSettings> appSettings,
    IBaseDefaultRepository<Article> articleRepo,
    IMailService mailService) : INotificationHandler<MessageReplyEmailEvent>
{
    public async Task Handle(MessageReplyEmailEvent notification, CancellationToken cancellationToken)
    {
        var source = notification.Source;
        var toEmail = source.Visitor.Email;
        if (string.IsNullOrWhiteSpace(toEmail)) return;
        var reply = notification.Reply ?? throw new ApplicationException("邮件推送中消息内容为空");
        var clientDomain = appSettings.CurrentValue?.ClientDomain;

        var subject = string.Empty;
        var body = string.Empty;
        var key = string.Empty;
        var link = string.Empty;
        switch (reply.CommentType)
        {
            case BelongType.Article:
                var article = await articleRepo.Select.Where(a => a.ArticleId == reply.BelongId).FirstAsync(cancellationToken);
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

        subject = $"{reply.Visitor.Nickname} 回复你了在{key}发起的评论";
        body = @$"
{source.Visitor.Nickname }，你好！
<br/>
<br/>{reply.Visitor.Nickname} 回复了你在{key}的评论：
<br/>
<br/>{source.Content}
<br/>
<br/>------------------------------------------
<br/>回复到：
<br/>
<br/>{reply.Content}
<br/>
<br/>------------------------------------------
<br/>访问链接：<a href='{link}'>{link}</a>
<br/>这是系统自动通知邮件，不需要回复哈。";


        // 异步发送邮件，不需要等待
        _ = mailService.SendAsync(new MailMsg
        {
            Subject = subject,
            Body = body,
            Tos = new List<string> { toEmail },
        });
    }
}
