using System.Net.Mail;
using System.Net;
using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Memo.Blog.Application.Common.Extensions;
using System.ComponentModel;

namespace Memo.Blog.Infrastructure.Mail;

public class MailService(
    ILogger<MailService> logger,
    IOptionsMonitor<AuthorizationSettings> authOptions
    ) : IMailService
{
    private readonly MailOptions _mailOptions = authOptions.CurrentValue?.Mail ?? throw new Exception("未配置服务mail");
    private Action<MailCompleted>? _sendCompletedCallback = null;

    public void Send(MailMsg msg, Action<MailCompleted>? callback = null)
    {
        // 未启用邮件推送提醒，则直接退出
        if (!_mailOptions.Enable) return;

        try
        {
            // 设置发送者的电子邮件地址和密码
            string senderEmail = _mailOptions.Email;
            string senderPassword = _mailOptions.Password;

            // 创建邮件对象
            var mail = new MailMessage();
            // 添加多个收件人的电子邮件地址
            foreach (var to in msg.Tos)
            {
                mail.To.Add(to);
            }
            mail.From = new MailAddress(senderEmail, "Blog通知");
            mail.Subject = msg.Subject;
            mail.Body = msg.Body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.High;

            // 创建SMTP客户端
            SmtpClient smtpClient = new(_mailOptions.Host)
            {
                Port = _mailOptions.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = _mailOptions.EnableSsl
            };

            // 发送邮件
            _sendCompletedCallback = callback;
            smtpClient.SendCompleted += SendCompletedCallback;
            if (callback != null)
            {
                smtpClient.SendAsync(mail, msg);
            }
            else
            {
                smtpClient.Send(mail);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "邮件发送异常");
        }
    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        if (_sendCompletedCallback == null) return;
        MailCompleted completed = new MailCompleted
        {
            Success = true,
            Message = string.Empty,
            UserState = e.UserState,
        };
        object? state = e.UserState;
        if (e.Cancelled)
        {
            logger.LogError("邮件发送异步操作取消");
            completed.Message = "邮件发送异步操作取消";
            completed.Success = false;
        }
        else if (e.Error != null)
        {
            logger.LogError($"邮件发送错误：{e.Error}, userState:{state?.ToJson()}");
            completed.Message = "邮件发送错误";
            completed.Success = false;
        }

        //执行回调方法
        _sendCompletedCallback(completed);
    }
}
