namespace Memo.Blog.Application.Comments.Commands.Update;

[Authorize(Permissions = ApiPermission.Comment.Update)]
[Transactional]
public record UpdateCommentCommand : IRequest<Result>
{
    public long CommentId { get; set; }

    public string Nickname { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool Showable { get; set; }
}

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
           .Must(x => x > 0)
           .WithMessage("评论Id不能小于0");


        RuleFor(x => x.Nickname)
           .MinimumLength(1)
           .MaximumLength(20)
           .WithMessage("评论昵称长度在1-20个字符之间");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("文章内容不能为空");
    }
}
