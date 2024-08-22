namespace Memo.Blog.Application.Configs.Common;

public record VisitorConfigResult
{
    public long UserId { get; set; }

    public long VisitorId { get; set; }

    public string Nickname { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
}
