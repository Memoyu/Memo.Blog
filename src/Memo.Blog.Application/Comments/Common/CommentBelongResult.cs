namespace Memo.Blog.Application.Comments.Common;

public class CommentBelongResult
{
    public long BelongId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;
}
