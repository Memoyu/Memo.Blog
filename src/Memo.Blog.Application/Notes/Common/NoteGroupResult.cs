namespace Memo.Blog.Application.Notes.Common;

public record NoteGroupResult
{
    /// <summary>
    /// 分组Id
    /// </summary>
    public long GroupId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;
}
