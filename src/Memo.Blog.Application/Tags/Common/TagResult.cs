namespace Memo.Blog.Application.Tags.Common;

public record TagResult
{
    public long TagId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;
}
