namespace Memo.Blog.Application.Articles.Commands.Create;

[Authorize(Permissions = ApiPermission.Article.Create)]
[Transactional]
public record CreateArticleCommand(
    long CategoryId,
    string Title,
    string Content
    ) : IRequest<Result>;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator(
        IBaseDefaultRepository<Role> roleResp
        )
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            // TODO: 检查分类是否存在
            .WithMessage("分类Id不存在");

        RuleFor(x => x.Title)
           .MinimumLength(1)
           .MaximumLength(100)
           .WithMessage("文章标题长度在1-100个字符之间");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("文章内容不能为空");
    }
}
