using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Commands.Create;

[Transactional]
public class CreateCommentCommand : IRequest<Result>
{
    /// <summary>
    /// 父评论Id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 回复评论Id
    /// </summary>
    public long? ReplyId { get; set; }

    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    public long BelongId { get; set; }

    /// <summary>
    /// 评论类型
    /// </summary>
    public CommentType CommentType { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

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

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.CommentType)
            .IsInEnum()
            .WithMessage("评论类型错误");

        RuleFor(x => x.Nickname)
            .NotEmpty()
            .WithMessage("昵称不能为空");

        RuleFor(x => x.Nickname)
            .MinimumLength(1)
            .MaximumLength(20)
            .WithMessage("昵称长度在1-20个字符之间");
    }
}



