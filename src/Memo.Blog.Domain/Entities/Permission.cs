namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 权限表
/// </summary>
[Table(Name = "permission")]
public class Permission : BaseAuditEntity
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Snowflake]
    [Description("权限Id")]
    public long PermissionId { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    [Description("权限名称")]
    [Column(StringLength = 60)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限所属模块
    /// </summary>
    [Description("权限所属模块")]
    [Column(StringLength = 50)]
    public string Module { get; set; } = string.Empty;
}
