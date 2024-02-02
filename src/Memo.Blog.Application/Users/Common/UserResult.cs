namespace Memo.Blog.Application.Users.Common;

public class UserResult
{
    public long Id { get; set; }

    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    ///  用户头像url
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 最后一次登录的时间
    /// </summary>
    public DateTime LastLoginTime { get; set; }
}
