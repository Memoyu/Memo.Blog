namespace Memo.Blog.Application.Configs.Common;

public record ConfigResult
{
    public AdminConfig Admin { get; set; } = new();

    public BannerConfig Banner { get; set; } = new();

    public ColorConfig Color { get; set; } = new();

    public string AdminJson { get; set; } = string.Empty;

    public string BannerJson { get; set; } = string.Empty;

    public string ColorJson { get; set; } = string.Empty;
}
