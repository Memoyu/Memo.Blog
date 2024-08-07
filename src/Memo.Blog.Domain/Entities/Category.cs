﻿namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章分类表
/// </summary>
[Table(Name = "category")]
[Index("index_on_category_id", nameof(CategoryId), false)]
public class Category : BaseAuditEntity
{
    /// <summary>
    /// 分类Id
    /// </summary>
    [Snowflake]
    [Description("分类Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long CategoryId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [Description("分类名称")]
    [Column(StringLength = 40, IsNullable = false)]
    public string Name { get; set; } = string.Empty;
}
