namespace Memo.Blog.Application.Logger.Queries.System.Get;

[Authorize(Permissions = ApiPermission.LoggerSystem.Get)]
public record GetLoggerSystemQuery(long CommentId) : IRequest<Result>;

public class GetLoggerSystemQueryValidator : AbstractValidator<GetLoggerSystemQuery>
{
    public GetLoggerSystemQueryValidator()
    {
        RuleFor(x => x.CommentId)
            .Must(x => x > 0)
            .WithMessage("评论Id必须大于0");
    }
}
