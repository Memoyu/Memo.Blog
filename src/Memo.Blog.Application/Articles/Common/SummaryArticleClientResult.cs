namespace Memo.Blog.Application.Articles.Common;

public record SummaryArticleClientResult
{
    public long Articles { get; set; }

    public long Moments { get; set; }

    public long Comments { get; set; }
}
