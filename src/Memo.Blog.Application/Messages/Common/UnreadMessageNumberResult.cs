namespace Memo.Blog.Application.Messages.Common;

public record UnreadMessageNumberResult
{
    public int Total { get; set; }

    public int User { get; set; }

    public int Comment { get; set; }

    public int Like { get; set; }

}
