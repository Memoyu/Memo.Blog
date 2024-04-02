using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Articles.Commands.Update;

[Authorize(Permissions = ApiPermission.Article.Update)]
[Transactional]
public record UpdateArticleCommand(
    long ArticleId,
    long CategoryId,
    List<long> Tags,
    string Title,
    string Description,
    string Content,
    string Banner,
    ArticleStatus Status,
    bool IsTop,
    bool Commentable,
    bool Publicable
    ) : IAuthorizeableRequest<Result>;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator()
    {
        RuleFor(x => x.ArticleId)
           .Must(x => x > 0)
           .WithMessage("文章Id不能小于0");

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

        RuleFor(x => x.Banner)
            .NotEmpty()
            .WithMessage("文章横幅图不能为空");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("文章状态错误");
    }
}
