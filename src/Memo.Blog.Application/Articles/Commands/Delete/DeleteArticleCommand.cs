namespace Memo.Blog.Application.Articles.Commands.Delete;

[Authorize(Permissions = ApiPermission.Article.Delete)]
[Transactional]
public record DeleteArticleCommand(long ArticleId) : IRequest<Result>;


public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    public DeleteArticleCommandValidator()
    {
        RuleFor(x => x.ArticleId)
            .Must(x => x > 0)
            .WithMessage("文章Id必须大于0");
    }
}
