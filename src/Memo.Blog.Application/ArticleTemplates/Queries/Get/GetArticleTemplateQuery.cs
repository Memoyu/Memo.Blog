namespace Memo.Blog.Application.ArticleTemplates.Queries.Get;

[Authorize(Permissions = ApiPermission.ArticleTemplate.Get)]
public record GetArticleTemplateQuery(long TemplateId) : IAuthorizeableRequest<Result>;

public class GetArticleTemplateQueryValidator : AbstractValidator<GetArticleTemplateQuery>
{
    public GetArticleTemplateQueryValidator()
    {
        RuleFor(x => x.TemplateId)
            .GreaterThan(0)
            .WithMessage("Id必须大于0");
    }
}
