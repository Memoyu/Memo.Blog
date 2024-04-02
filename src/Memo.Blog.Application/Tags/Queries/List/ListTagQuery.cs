namespace Memo.Blog.Application.Tags.Queries.List;

public record ListTagQuery(
    string Name
    ) : IAuthorizeableRequest<Result>;

public class ListTagQueryValidator : AbstractValidator<ListTagQuery>
{
    public ListTagQueryValidator()
    {
    }
}


