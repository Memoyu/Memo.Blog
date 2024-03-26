using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 用户角色表
/// </summary>
[Table(Name = "role")]
[Index("index_on_role_id", nameof(RoleId), false)]
public class Role : BaseAuditEntity
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [Snowflake]
    [Description("角色Id")]
    public long RoleId { get; set; }

    /// <summary>
    /// 角色唯一标识字符
    /// </summary>
    [Description("角色唯一标识字符")]
    [Column(StringLength = 60)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 角色类型
    /// </summary>
    [Description("角色类型")]
    public RoleType Type { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    [Description("角色描述")]
    [Column(StringLength = 100)]
    public string Description { get; set; } = string.Empty;
}
