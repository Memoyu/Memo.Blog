namespace Memo.Blog.Application.Configs.Common;

public record ConfigResult
{
    public BannerConfig Banner { get; set; } = new();

    public ColorConfig Color { get; set; } = new();

    public string BannerJson { get; set; } = string.Empty;

    public string ColorJson { get; set; } = string.Empty;
}
