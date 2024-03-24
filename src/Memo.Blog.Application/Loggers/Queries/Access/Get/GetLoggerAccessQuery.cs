namespace Memo.Blog.Application.Logger.Queries.Access.Get;

[Authorize(Permissions = ApiPermission.LoggerAccess.Get)]
public record GetLoggerAccessQuery(long CommentId) : IRequest<Result>;

public class GetLoggerAccessQueryValidator : AbstractValidator<GetLoggerAccessQuery>
{
    public GetLoggerAccessQueryValidator()
    {
        RuleFor(x => x.CommentId)
            .Must(x => x > 0)
            .WithMessage("评论Id必须大于0");
    }
}
