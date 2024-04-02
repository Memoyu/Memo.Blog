namespace Memo.Blog.Application.Articles.Queries.Get;

[Authorize(Permissions = ApiPermission.Article.Get)]
public record GetArticleQuery(long ArticleId) : IAuthorizeableRequest<Result>;

public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
{
    public GetArticleQueryValidator()
    {
        RuleFor(x => x.ArticleId)
            .Must(x => x > 0)
            .WithMessage("文章Id必须大于0");
    }
}
