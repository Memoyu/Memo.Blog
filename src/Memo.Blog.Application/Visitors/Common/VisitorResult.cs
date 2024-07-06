namespace Memo.Blog.Application.Visitors.Common;

public class VisitorResult : VisitorClientResult
{
    /// <summary>
    /// 头像来源类型
    /// </summary>
    public AvatarOriginType? AvatarOriginType { get; set; }

    /// <summary>
    /// 头像来源
    /// </summary>
    public string? AvatarOrigin { get; set; }

    /// <summary>
    /// 访客归属
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
