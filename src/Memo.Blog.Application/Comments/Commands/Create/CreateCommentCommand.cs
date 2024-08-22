using Memo.Blog.Application.Comments.Common;

namespace Memo.Blog.Application.Comments.Commands.Create;

[Authorize(Permissions = ApiPermission.Comment.Create)]
[Transactional]
public record CreateCommentCommand : BaseCreateCommentCommand, IAuthorizeableRequest<Result>
{
    /// <summary>
    /// 回复使用的访客Id
    /// </summary>
    public long VisitorId { get; set; }
}

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.VisitorId)
            .NotEmpty()
            .WithMessage("回复使用的访客Id不能为空");

        RuleFor(x => x.CommentType)
         .IsInEnum()
         .WithMessage("评论类型错误");
    }
}

[Transactional]
public record CreateCommentClientCommand : BaseCreateCommentCommand, IRequest<Result>
{
}

public class CreateCommentClientCommandValidator : AbstractValidator<CreateCommentClientCommand>
{
    public CreateCommentClientCommandValidator()
    {
        RuleFor(x => x.CommentType)
            .IsInEnum()
            .WithMessage("评论类型错误");
    }
}

[Transactional]
public record CommonCreateCommentCommand : BaseCreateCommentCommand, IRequest<CommentClientResult>
{
    /// <summary>
    /// 回复使用的访客Id
    /// </summary>
    public long VisitorId { get; set; }
}


public record BaseCreateCommentCommand
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
    public BelongType CommentType { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 排除的接收用户Id
    /// </summary>
    public List<long>? ExcludeUsers { get; set; }
}



