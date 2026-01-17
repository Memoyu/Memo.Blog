namespace Memo.Blog.Application.Notes.Common;

public record NoteResult
{
    public long NoteId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 笔记正文
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 笔记所属目录
    /// </summary>
    public NoteCatalogResult Catalog { get; set; } = new();

    /// <summary>
    /// 笔记作者
    /// </summary>
    public NoteAuthorResult Author { get; set; } = new();

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}
