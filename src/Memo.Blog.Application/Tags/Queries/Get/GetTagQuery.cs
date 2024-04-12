namespace Memo.Blog.Application.Tags.Queries.Get;

[Authorize(Permissions = ApiPermission.Tag.Get)]
public record GetTagQuery(long TagId) : IAuthorizeableRequest<Result>;

public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
{
    public GetTagQueryValidator()
    {
        RuleFor(x => x.TagId)
            .GreaterThan(0)
            .WithMessage("Id必须大于0");
    }
}


