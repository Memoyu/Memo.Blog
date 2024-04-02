namespace Memo.Blog.Application.Articles.Commands.Published;

[Authorize(Permissions = ApiPermission.Article.Publish)]
[Transactional]
public record PublishArticleCommand(long ArticleId) : IAuthorizeableRequest<Result>;

public class PublishArticleCommandValidator : AbstractValidator<PublishArticleCommand>
{
    public PublishArticleCommandValidator()
    {
        RuleFor(x => x.ArticleId)
            .Must(x => x > 0)
            .WithMessage("文章Id必须大于0");
    }
}
