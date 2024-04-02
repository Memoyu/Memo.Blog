namespace Memo.Blog.Application.Comments.Commands.Delete;

[Authorize(Permissions = ApiPermission.Comment.Delete)]
[Transactional]
public record DeleteCommentCommand(long CommentId) : IAuthorizeableRequest<Result>;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .Must(x => x > 0)
            .WithMessage("评论Id必须大于0");
    }
}
