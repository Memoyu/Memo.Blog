using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Visitors.Commands.Generate;

public record CreateVisitorCommand : IRequest<Result>
{
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
