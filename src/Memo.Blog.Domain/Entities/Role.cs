﻿namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 用户角色表
/// </summary>
[Table(Name = "role")]
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
    public int Type { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    [Description("角色描述")]
    [Column(StringLength = 100)]
    public string Info { get; set; } = string.Empty;

    /// <summary>
    /// 排序码，升序
    /// </summary>
    [Description("排序码，升序")]
    public int Sort { get; set; }
}