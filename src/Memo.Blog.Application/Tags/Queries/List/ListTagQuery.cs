namespace Memo.Blog.Application.Tags.Queries.List;

[Authorize(Permissions = ApiPermission.Tag.List)]
public record ListTagQuery(string Name) : IAuthorizeableRequest<Result>;

public class ListTagQueryValidator : AbstractValidator<ListTagQuery>
{
    public ListTagQueryValidator()
    {
    }
}


