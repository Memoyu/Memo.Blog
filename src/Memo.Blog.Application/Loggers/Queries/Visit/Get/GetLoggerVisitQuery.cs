namespace Memo.Blog.Application.Logger.Queries.Visit.Get;

[Authorize(Permissions = ApiPermission.LoggerVisit.Get)]
public record GetLoggerVisitQuery(long CommentId) : IAuthorizeableRequest<Result>;

public class GetLoggerVisitQueryValidator : AbstractValidator<GetLoggerVisitQuery>
{
    public GetLoggerVisitQueryValidator()
    {
        RuleFor(x => x.CommentId)
            .Must(x => x > 0)
            .WithMessage("评论Id必须大于0");
    }
}
