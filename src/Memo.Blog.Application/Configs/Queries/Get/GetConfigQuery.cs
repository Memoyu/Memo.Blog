namespace Memo.Blog.Application.Configs.Queries.Get;

[Authorize(Permissions = ApiPermission.Config.Get)]
public record GetConfigQuery(
    long ConfigId
    ) : IAuthorizeableRequest<Result>;

public class GetConfigQueryValidator : AbstractValidator<GetConfigQuery>
{
    public GetConfigQueryValidator()
    {
        RuleFor(x => x.ConfigId)
            .GreaterThan(0)
            .WithMessage("Id必须大于0");
    }
}
