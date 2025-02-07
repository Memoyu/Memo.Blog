﻿namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 开源项目表
/// </summary>
[Table(Name = "open_source")]
[Index("index_on_project_id", nameof(ProjectId), false)]
public class OpenSource : BaseAuditEntity
{
    /// <summary>
    /// 项目Id
    /// </summary>
    [Snowflake]
    [Description("项目Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long ProjectId { get; set; }

    /// <summary>
    /// 源项目Id
    /// </summary>
    public long? RepoId { get; set; }

    /// <summary>
    /// 项目标题
    /// </summary>
    [Description("项目标题")]
    [Column(StringLength = 100, IsNullable = false)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    [Description("项目描述")]
    [Column(StringLength = 300, IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///  项目图片
    /// </summary>
    [Description("项目图片")]
    [Column(IsNullable = false)]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    ///  项目Readme文档源地址
    /// </summary>
    [Description("项目Readme文档源地址")]
    [Column(IsNullable = false)]
    public string ReadmeUrl { get; set; } = string.Empty;

    /// <summary>
    ///  项目源地址
    /// </summary>
    [Description("项目源地址")]
    [Column(IsNullable = false)]
    public string HtmlUrl { get; set; } = string.Empty;

    /// <summary>
    /// Star次数
    /// </summary>
    [Description("Star次数")]
    [Column(IsNullable = false)]
    public int Star { get; set; }

    /// <summary>
    /// Fork次数
    /// </summary>
    [Description("Fork次数")]
    [Column(IsNullable = false)]
    public int Fork { get; set; }
}
