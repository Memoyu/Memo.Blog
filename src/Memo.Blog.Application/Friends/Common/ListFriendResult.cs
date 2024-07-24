namespace Memo.Blog.Application.Friends.Common;

public record ListFriendResult
{
    /// <summary>
    /// 友链Id
    /// </summary>
    public long FriendId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 站点链接
    /// </summary>
    public string Site { get; set; } = string.Empty;

    /// <summary>
    ///  头像url
    /// </summary>
    public string Avatar { get; set; } = string.Empty;
}
