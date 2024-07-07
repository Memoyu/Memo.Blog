namespace Memo.Blog.Application.Articles.Commands.Create;

[Authorize(Permissions = ApiPermission.Article.Create)]
[Transactional]
public record CreateArticleCommand(
    long CategoryId,
    List<long> Tags,
    string Title,
    string Description,
    string Content,
    string Banner,
    bool IsTop,
    bool Commentable,
    bool Publicable,
    ArticleStatus? Status
    ) : IAuthorizeableRequest<Result>;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Tags)
           .NotEmpty()
           .WithMessage("标签不能为空");

        RuleFor(x => x.Title)
           .MinimumLength(1)
           .MaximumLength(100)
           .WithMessage("文章标题长度在1-100个字符之间");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("文章内容不能为空");

        //RuleFor(x => x.Banner)
        //    .NotEmpty()
        //    .WithMessage("文章横幅图不能为空");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("文章状态错误");
    }
}
