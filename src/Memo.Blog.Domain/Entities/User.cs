namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 用户表
/// </summary>
[Table(Name = "user")]
[Index("index_on_user_id", nameof(UserId), false)]
public class User : BaseAuditEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [Snowflake]
    [Description("用户Id")]
    [Column(CanUpdate = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("用户名")]
    [Column(StringLength = 50)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    [Description("用户昵称")]
    [Column(StringLength = 50)]
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    ///  用户头像url
    /// </summary>
    [Description("用户头像")]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [Description("电子邮箱")]
    [Column(StringLength = 100)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    [Description("手机号")]
    [Column(StringLength = 100)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 最后一次登录的时间
    /// </summary>
    [Description("最后一次登录的时间")]
    public DateTime LastLoginTime { get; set; }
}
