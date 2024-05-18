namespace Memo.Blog.Application.Tags.Common;

public record TagWithArticleCountResult : TagResult
{
    public int Articles { get; set; }
}
