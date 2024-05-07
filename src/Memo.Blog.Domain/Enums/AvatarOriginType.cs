namespace Memo.Blog.Domain.Enums;

/// <summary>
/// 头像来源
/// </summary>
public enum AvatarOriginType
{
    [Description("未知")]
    Unknown = 0,

    [Description("QQ")]
    Qq = 1,

    [Description("GitHub")]
    Github = 2,

    [Description("链接")]
    Upload = 3,
}
