namespace Memo.Blog.Application.Configs.Common;

public record ConfigResult
{
    public BannerConfigResult Banner { get; set; } = new();

    public ColorConfigResult Color { get; set; } = new();

    public string BannerJson { get; set; } = string.Empty;

    public string ColorJson { get; set; } = string.Empty;
}

public record BannerConfigResult
{
    public string Home { get; set; } = string.Empty;

    public string Article { get; set; } = string.Empty;

    public string Lab { get; set; } = string.Empty;

    public string Moment { get; set; } = string.Empty;

    public string About { get; set; } = string.Empty;
}


public record ColorConfigResult
{
    public List<string> Primary { get; set; } = [];

    public List<string> Secondary { get; set; } = [];

    public List<string> Tertiary { get; set; } = [];
}
