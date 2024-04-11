﻿namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 访客信息表
/// </summary>
[Table(Name = "visitor")]
public class Visitor : BaseAuditEntity
{
    /// <summary>
    /// 访问者标识Id
    /// </summary>
    [Snowflake]
    [Description("当日UV总数")]
    [Column(IsNullable = false)]
    public long VisitorId { get; set; }

    /// <summary>
    /// 访问者所在IP
    /// </summary>
    [Description("访问者所在IP")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属国家
    /// </summary>
    [Description("访问者IP所属国家")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属区域
    /// </summary>
    [Description("访问者IP所属区域")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属省市
    /// </summary>
    [Description("访问者IP所属省市")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属城市
    /// </summary>
    [Description("访问者IP所属城市")]
    [Column(StringLength = 50, IsNullable = false)]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属互联网服务商
    /// </summary>
    [Description("访问者IP所属互联网服务商")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Isp { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统
    /// </summary>
    [Description("操作系统")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Os { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器
    /// </summary>
    [Description("浏览器")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Browser { get; set; } = string.Empty;
}