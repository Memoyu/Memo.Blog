namespace Memo.Blog.Application.Common.Models.Mail;

public class MailCompleted
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public object? UserState { get; set; }
}
