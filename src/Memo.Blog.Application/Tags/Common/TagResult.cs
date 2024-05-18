namespace Memo.Blog.Application.Tags.Common;

public record TagResult
{
    public long TagId { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }
}
