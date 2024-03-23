namespace Memo.Blog.Application.Comments.Queries.Get;

[Authorize(Permissions = ApiPermission.Comment.Get)]
public record GetCommentQuery(long CommentId) : IRequest<Result>;

public class GetCommentQueryValidator : AbstractValidator<GetCommentQuery>
{
    public GetCommentQueryValidator()
    {
        RuleFor(x => x.CommentId)
            .Must(x => x > 0)
            .WithMessage("评论Id必须大于0");
    }
}
