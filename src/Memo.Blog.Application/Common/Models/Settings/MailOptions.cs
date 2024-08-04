namespace Memo.Blog.Application.Common.Models.Settings;

public class MailOptions
{
    /// <summary>
    /// 是否启用邮件推送提醒
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 发件人用户名
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 发件人邮箱密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 发送服务器地址
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// 发送服务器端口号，默认25
    /// </summary>
    public int Port { get; set; } = 25;

    /// <summary>
    /// 是否启用SSL，默认已启用
    /// </summary>
    public bool EnableSsl { get; set; } = true;

}
