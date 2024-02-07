namespace Memo.Blog.Application.Categories.Queries.Get;

public record ListCategoryQuery(
    string Name
    ) : IRequest<Result>;

public class ListCategoryQueryValidator : AbstractValidator<ListCategoryQuery>
{
    public ListCategoryQueryValidator()
    {
    }
}


