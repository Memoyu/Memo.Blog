namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 笔记表
/// </summary>
[Table(Name = "note")]
[Index("index_on_note_id", nameof(NoteId), false)]
public class Note : BaseAuditEntity
{
    /// <summary>
    /// 笔记Id
    /// </summary>
    [Snowflake]
    [Description("笔记Id")]
    [Column(IsNullable = false)]
    public long NoteId { get; set; }

    /// <summary>
    /// 所属目录Id
    /// </summary>
    [Description("所属目录Id")]
    [Column(IsNullable = false)]
    public long CatalogId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    [Column(StringLength = 200, IsNullable = false)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 笔记正文
    /// </summary>
    [Description("笔记正文")]
    [Column(StringLength = -2, IsNullable = false)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 笔记所属目录
    /// </summary>
    [Navigate(nameof(CatalogId), TempPrimary = nameof(NoteCatalog.CatalogId))]
    public virtual NoteCatalog Catalog { get; set; } = new();

    /// <summary>
    /// 笔记作者
    /// </summary>
    [Navigate(nameof(CreateUserId), TempPrimary = nameof(User.UserId))]
    public virtual User Author { get; set; } = new();
}
