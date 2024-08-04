namespace Memo.Blog.Application.Common.Models.Mail;

public record MailMsg
{
    /// <summary>
    /// 邮件标题
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 邮件正文
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// 收件人
    /// </summary>
    public List<string> Tos { get; set; } = [];
}
