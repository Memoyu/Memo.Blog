namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 用户角色与权限关联表
/// </summary>
[Table(Name = "role_permission")]
public class RolePermission : BaseAuditEntity
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [Description("角色Id")]
    public long RoleId { get; set; }

    /// <summary>
    /// 权限Id
    /// </summary>
    [Description("权限Id")]
    public long PermissionId { get; set; }
}
