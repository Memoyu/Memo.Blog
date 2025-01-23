using Memo.Blog.Application.Common.Models.Mail;

namespace Memo.Blog.Application.Common.Interfaces.Services.Mail;

public interface IMailService
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="callback"></param>
    Task SendAsync(MailMsg msg, Action<MailCompleted>? callback = null);
}
