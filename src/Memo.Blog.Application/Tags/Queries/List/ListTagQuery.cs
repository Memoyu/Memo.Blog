namespace Memo.Blog.Application.Tags.Queries.Get;

public record ListTagQuery(
    string Name
    ) : IRequest<Result>;

public class ListTagQueryValidator : AbstractValidator<ListTagQuery>
{
    public ListTagQueryValidator()
    {
    }
}


