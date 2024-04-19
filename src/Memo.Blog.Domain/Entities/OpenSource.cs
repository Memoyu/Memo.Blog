namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 开源项目表
/// </summary>
[Table(Name = "open_source")]
[Index("index_on_project_id", nameof(ProjectId), false)]
public class OpenSource
{
    /// <summary>
    /// 项目Id
    /// </summary>
    [Snowflake]
    [Description("项目Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long ProjectId { get; set; }

    /// <summary>
    /// Github项目Id
    /// </summary>
    [Description("Github项目Id")]
    [Column(CanUpdate = false, IsNullable = false)]
    public long RepoId { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    [Description("项目标题")]
    [Column(StringLength = 100, IsNullable = false)]
    public string RepoName { get; set; } = string.Empty;

    /// <summary>
    /// 项目全称
    /// </summary>
    [Description("项目全称")]
    [Column(StringLength = 100, IsNullable = false)]
    public string RepoFullName { get; set; } = string.Empty;

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
