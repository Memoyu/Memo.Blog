using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Loggers.Common;

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
}
