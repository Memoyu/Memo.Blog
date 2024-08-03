namespace Memo.Blog.Application.ArticleTemplates.Queries.List;

[Authorize(Permissions = ApiPermission.ArticleTemplate.List)]
public record ListArticleTemplateQuery(string Name) : IAuthorizeableRequest<Result>;

public class ListArticleTemplateQueryValidator : AbstractValidator<ListArticleTemplateQuery>
{
    public ListArticleTemplateQueryValidator()
    {
    }
}
