namespace Memo.Blog.Application.Articles.Queries.Get;

public record PageArticleQuery(string? Title, long? CategoryId, List<long>? TagIds) : PaginationQuery, IRequest<Result>;

public class PageArticleQueryValidator : AbstractValidator<PageArticleQuery>
{
    public PageArticleQueryValidator()
    {
    }
}
