namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 用户与角色关联表
/// </summary>
[Table(Name = "user_role")]
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
}
