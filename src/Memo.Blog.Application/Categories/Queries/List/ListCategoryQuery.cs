namespace Memo.Blog.Application.Categories.Queries.List;

[Authorize(Permissions = ApiPermission.Category.List)]
public record ListCategoryQuery(string Name) : IAuthorizeableRequest<Result>;

public class ListCategoryQueryValidator : AbstractValidator<ListCategoryQuery>
{
    public ListCategoryQueryValidator()
    {
    }
}

public record ListCategoryClientQuery(string Name) : IRequest<Result>;
