namespace Memo.Blog.Domain.Enums;

/// <summary>
/// 头像来源
/// </summary>
public enum AvatarOriginType
{
    [Description("未知来源")]
    Unknown = 0,

    [Description("用户上传")]
    Upload = 1,

    [Description("来源github")]
    Github = 2,

    [Description("来源QQ")]
    Qq = 3,
}
