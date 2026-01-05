namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 笔记目录表
/// </summary>
[Table(Name = "note_catalog")]
[Index("index_on_catalog_id", nameof(CatalogId), false)]
public class NoteCatalog : BaseAuditEntity
{
    /// <summary>
    /// 目录Id
    /// </summary>
    [Snowflake]
    [Description("目录Id")]
    [Column(IsNullable = false)]
    public long CatalogId { get; set; }

    /// <summary>
    /// 父目录Id
    /// </summary>
    [Description("父目录Id")]
    public long? ParentId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    [Column(StringLength = 200, IsNullable = false)]
    public string Title { get; set; } = string.Empty;
}
