using Memo.Blog.Application.Articles.Queries.Page;

namespace Memo.Blog.Application.Articles.Queries.Summary;

public record PageSummaryArticleQuery : PageArticleQuery, IRequest<Result>;

public class ListSummaryArticleQueryValidator : AbstractValidator<PageSummaryArticleQuery>
{
    public ListSummaryArticleQueryValidator()
    {
    }
}
