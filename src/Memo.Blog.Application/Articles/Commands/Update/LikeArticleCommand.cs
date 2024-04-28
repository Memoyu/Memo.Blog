namespace Memo.Blog.Application.Articles.Commands.Update;

public record LikeArticleCommand(long ArticleId) : IRequest<Result>;

public class LikeArticleCommandValidator : AbstractValidator<LikeArticleCommand>
{
    public LikeArticleCommandValidator()
    {
        RuleFor(x => x.ArticleId)
            .Must(x => x > 0)
            .WithMessage("文章Id必须大于0");
    }
}
