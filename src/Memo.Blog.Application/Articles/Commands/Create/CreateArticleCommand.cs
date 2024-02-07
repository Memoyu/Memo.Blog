using Memo.Blog.Domain.Enums;

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
    ArticleStatus Status,
    bool IsTop,
    bool Commentable,
    bool Publicable
    ) : IRequest<Result>;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator(
        IBaseDefaultRepository<Category> categoryResp,
        IBaseDefaultRepository<Tag> tagResp
        )
    {
        RuleFor(x => x.CategoryId)
            .MustAsync(async (x, ct) => x > 0 && !await categoryResp.Select.AnyAsync(t => x == t.CategoryId, ct))
            .WithMessage("文章分类不存在");

        RuleFor(x => x.Tags)
          .NotEmpty()
          .WithMessage("标签不能为空");

        RuleForEach(x => x.Tags)
           .MustAsync(async (x, ct) => x > 0 && await tagResp.Select.AnyAsync(t => x == t.TagId, ct))
           .WithMessage((_, p) => $"标签: {p}不存在");

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
