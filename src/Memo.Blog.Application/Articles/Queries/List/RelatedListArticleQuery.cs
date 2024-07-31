namespace Memo.Blog.Application.Articles.Queries.List;

[Authorize(Permissions = ApiPermission.Article.RelatedList)]
public record RelatedListArticleQuery : IAuthorizeableRequest<Result>
{
    /// <summary>
    /// 1： 分类，2： 标签
    /// </summary>
    public int Type { get; set; }

    public long Id { get; set; }
}

public class RelatedListArticleQueryValidator : AbstractValidator<RelatedListArticleQuery>
{
    public RelatedListArticleQueryValidator()
    {
        RuleFor(x => x.Type)
          .Must(x => x > 0)
          .WithMessage("类型必须大于0");

        RuleFor(x => x.Id)
           .Must(x => x > 0)
           .WithMessage("关联Id必须大于0");
    }
}

