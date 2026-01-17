namespace Memo.Blog.Application.Notes.Common;

public record NoteCatalogResult
{
    /// <summary>
    /// 目录Id
    /// </summary>
    public long CatalogId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;
}
