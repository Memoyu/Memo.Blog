namespace Memo.Blog.Application.Categories.Queries.Get;

public record GetCategoryQuery(
    long CategoryId
    ) : IRequest<Result>;

public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Id必须大于0");
    }
}
