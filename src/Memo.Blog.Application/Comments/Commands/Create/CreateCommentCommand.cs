using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Commands.Create;

[Transactional]
public class CreateCommentClientCommand : IRequest<Result>
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
    /// 访客Id
    /// </summary>
    public long VisitorId { get; set; }

    /// <summary>
    /// 评论类型
    /// </summary>
    public CommentType CommentType { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;
}

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentClientCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.CommentType)
            .IsInEnum()
            .WithMessage("评论类型错误");

        RuleFor(x => x.VisitorId)
           .Must(x => x > 0)
           .WithMessage("访客Id不能小于0");

    }
}



