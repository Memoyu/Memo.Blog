using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;

namespace Memo.Blog.Infrastructure.Mail;

public class MailService(
    ILogger<MailService> logger,
    IOptionsMonitor<AuthorizationSettings> authOptions
    ) : IMailService
{
    private readonly MailOptions _mailOptions = authOptions.CurrentValue?.Mail ?? throw new Exception("未配置服务mail");

    public async Task SendAsync(MailMsg msg, Action<MailCompleted>? callback = null)
    {
        // 未启用邮件推送提醒，则直接退出
        if (!_mailOptions.Enable) return;

        try
        {
            string senderEmail = _mailOptions.Email;
            string senderPassword = _mailOptions.Password;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailOptions.DisplayName, senderEmail));
            foreach (var to in msg.Tos)
            {
                message.To.Add(new MailboxAddress(to, to));
            }

            message.Subject = msg.Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = msg.Body;
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //smtp服务器，端口，是否开启ssl
            await client.ConnectAsync(_mailOptions.Host, _mailOptions.Port, true);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            var res = await client.SendAsync(message);
            client.Disconnect(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "邮件发送异常");
        }
    }
}
