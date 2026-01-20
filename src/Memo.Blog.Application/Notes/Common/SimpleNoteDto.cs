namespace Memo.Blog.Application.Notes.Common;

internal record SimpleNoteDto
{
    public long NoteId { get; set; }

    /// <summary>
    /// 分组Id
    /// </summary>
    public long? GroupId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;
}
