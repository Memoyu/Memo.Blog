namespace Memo.Blog.Application.Moments.Queries.Get;

[Authorize(Permissions = ApiPermission.Moment.Get)]
public record GetMomentQuery(long MomentId) : IAuthorizeableRequest<Result>;

public class GetMomentQueryValidator : AbstractValidator<GetMomentQuery>
{
    public GetMomentQueryValidator()
    {
        RuleFor(x => x.MomentId)
            .Must(x => x > 0)
            .WithMessage("动态Id必须大于0");
    }
}
