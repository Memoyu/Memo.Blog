using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Visitors.Commands.Update;

public record UpdateVisitorCommand : IRequest<Result>
{
    /// <summary>
    /// 访客Id
    /// </summary>
    public long? VisitorId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

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

public class UpdateVisitorCommandValidator : AbstractValidator<UpdateVisitorCommand>
{
    public UpdateVisitorCommandValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .WithMessage("访客昵称不能为空");
    }
}

