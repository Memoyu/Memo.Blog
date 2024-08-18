namespace Memo.Blog.Application.Configs.Common;

public record BannerConfig
{
    public BannerInfo Home { get; set; } = new();

    public BannerInfo Article { get; set; } = new();

    public BannerInfo Lab { get; set; } = new();

    public BannerInfo Moment { get; set; } = new();

    public BannerInfo About { get; set; } = new();
}

public record BannerInfo 
{
    public string Url { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string OriginUrl { get; set; } = string.Empty;
}
