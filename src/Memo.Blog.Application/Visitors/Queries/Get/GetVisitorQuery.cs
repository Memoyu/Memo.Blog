namespace Memo.Blog.Application.Visitors.Queries.Get;

[Authorize(Permissions = ApiPermission.Visitor.Get)]
public record GetVisitorQuery(long VisitorId) : IAuthorizeableRequest<Result>;


public class GetVisitorQueryValidator : AbstractValidator<GetVisitorQuery>
{
    public GetVisitorQueryValidator()
    {
        RuleFor(x => x.VisitorId)
            .Must(x => x > 0)
            .WithMessage("访客Id必须大于0");
    }
}

