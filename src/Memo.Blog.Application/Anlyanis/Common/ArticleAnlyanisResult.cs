namespace Memo.Blog.Application.Anlyanis.Common;

public record ArticleAnlyanisResult
{
    public long ArticleId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Views { get; set; }

    public int Comments { get; set; }
}
