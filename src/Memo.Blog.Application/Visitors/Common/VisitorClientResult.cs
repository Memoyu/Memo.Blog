namespace Memo.Blog.Application.Visitors.Common;

public class VisitorClientResult
{
    /// <summary>
    /// 访客Id
    /// </summary>
    public long VisitorId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? Nickname { get; set; }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///  头像url
    /// </summary>
    public string? Avatar { get; set; }
}
