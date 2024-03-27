using Memo.Blog.Application.Permissions.Common;
using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Roles.Common;

public class RoleWithPermissionResult
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 角色唯一标识字符
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 角色类型
    /// </summary>
    public RoleType Type { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    public string Info { get; set; } = string.Empty;

    /// <summary>
    /// 关联权限
    /// </summary>
    public List<PermissionResult> Permissions { get; set; } = [];
}
