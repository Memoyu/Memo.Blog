namespace Memo.Blog.Application.Friends.Common;

public record PageFriendResult
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

    /// <summary>
    /// 是否展示
    /// </summary>
    public bool Showable { get; set; }

    /// <summary>
    /// 点击访问次数
    /// </summary>
    public int Views { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
