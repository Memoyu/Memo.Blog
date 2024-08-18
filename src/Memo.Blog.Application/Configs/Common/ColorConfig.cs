namespace Memo.Blog.Application.Configs.Common;

public record ColorConfig
{
    public List<string> Primary { get; set; } = [];

    public List<string> Secondary { get; set; } = [];

    public List<string> Tertiary { get; set; } = [];
}
