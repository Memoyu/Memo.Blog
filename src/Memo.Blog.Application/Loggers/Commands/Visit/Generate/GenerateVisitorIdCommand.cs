using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Loggers.Commands.Visit.Generate;

public record GenerateVisitorIdCommand : IRequest<Result>
{
    /// <summary>
    /// 操作系统
    /// </summary>
    public string Os { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器
    /// </summary>
    public string Browser { get; set; } = string.Empty;

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

    /// <summary>
    /// 头像来源类型
    /// </summary>
    public AvatarOriginType? AvatarOriginType { get; set; }

    /// <summary>
    /// 头像来源
    /// </summary>
    public string? AvatarOrigin { get; set; }
}
