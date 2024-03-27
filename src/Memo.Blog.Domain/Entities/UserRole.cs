﻿namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 用户与角色关联表
/// </summary>
[Table(Name = "user_role")]
[Index("index_on_user_id", nameof(UserId), false)]
[Index("index_on_role_id", nameof(RoleId), false)]
public class UserRole : BaseAuditEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [Description("用户Id")]
    public long UserId { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    [Description("角色Id")]
    public long RoleId { get; set; }

    /// <summary>
    /// 权限
    /// </summary>
    [Navigate(nameof(User.UserId), TempPrimary = nameof(UserId))]
    public virtual User User { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    [Navigate(nameof(Role.RoleId), TempPrimary = nameof(Role))]
    public virtual Role Role { get; set; }
}
