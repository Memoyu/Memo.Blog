using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Entities;

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
    [Description("访问者标识Id")]
    [Column(IsNullable = false)]
    public long VisitorId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [Description("昵称")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    ///  头像url
    /// </summary>
    [Description("头像url")]
    [Column(IsNullable = false)]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [Description("电子邮箱")]
    [Column(StringLength = 100, IsNullable = false)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 头像来源类型
    /// </summary>
    [Description("头像来源类型")]
    [Column(IsNullable = false)]
    public AvatarOriginType AvatarOriginType { get; set; }

    /// <summary>
    /// 头像来源
    /// </summary>
    [Description("头像来源")]
    [Column(IsNullable = false)]
    public string AvatarOrigin { get; set; } = string.Empty;

    /// <summary>
    /// 访问者所在IP
    /// </summary>
    [Description("访问者所在IP")]
    [Column(StringLength = 50, IsNullable = false)]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 评论IP所属
    /// </summary>
    [Description("评论IP所属")]
    [Column(StringLength = 100, IsNullable = false)]
    public string Region { get; set; } = string.Empty;
}
