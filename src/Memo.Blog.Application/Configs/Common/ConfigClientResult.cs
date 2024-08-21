namespace Memo.Blog.Application.Configs.Common;

public record ConfigClientResult
{
    public BannerConfig Banner { get; set; } = new();

    public ColorConfig Color { get; set; } = new();
}
