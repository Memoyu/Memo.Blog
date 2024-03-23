namespace Memo.Blog.Application.Categories.Queries.Get;

[Authorize(Permissions = ApiPermission.Category.List)]
public record ListCategoryQuery(
    string Name
    ) : IRequest<Result>;

public class ListCategoryQueryValidator : AbstractValidator<ListCategoryQuery>
{
    public ListCategoryQueryValidator()
    {
    }
}


