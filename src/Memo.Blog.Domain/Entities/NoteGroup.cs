namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 笔记分组表
/// </summary>
[Table(Name = "note_group")]
[Index("index_on_group_id", nameof(GroupId), false)]
public class NoteGroup : BaseAuditEntity
{
    /// <summary>
    /// 分组Id
    /// </summary>
    [Snowflake]
    [Description("分组Id")]
    [Column(IsNullable = false)]
    public long GroupId { get; set; }

    /// <summary>
    /// 父分组Id
    /// </summary>
    [Description("父分组Id")]
    public long? ParentId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    [Column(StringLength = 200, IsNullable = false)]
    public string Title { get; set; } = string.Empty;
}
