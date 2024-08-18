namespace Memo.Blog.Application.Configs.Common;

public record BannerConfig
{
    public string Home { get; set; } = string.Empty;

    public string Article { get; set; } = string.Empty;

    public string Lab { get; set; } = string.Empty;

    public string Moment { get; set; } = string.Empty;

    public string About { get; set; } = string.Empty;
}
