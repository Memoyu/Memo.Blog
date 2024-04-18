namespace Memo.Blog.Application.Categories.Common;

public record CategoryWithArticleCountResult
{
    public long CategoryId { get; set; }

    public string Name { get; set; }

    public int Articles { get; set; }
}
