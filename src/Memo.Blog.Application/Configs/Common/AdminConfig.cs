namespace Memo.Blog.Application.Configs.Common;
public record AdminConfig
{
    public VisitorConfig Visitor { get; set; } = new();
}

public record VisitorConfig
{
    public long VisitorId { get; set; }

    public string Nickname { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
}
