namespace Memo.Blog.Application.ArticleTemplates.Commands.Create;

[Authorize(Permissions = ApiPermission.ArticleTemplate.Create)]
public record CreateArticleTemplateCommand(string Name, string? Content) : IAuthorizeableRequest<Result>;

public class CreateArticleTemplateCommandValidator : AbstractValidator<CreateArticleTemplateCommand>
{
    public CreateArticleTemplateCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("文章模板名称不能为空");

        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(20)
            .WithMessage("分类名称长度在1-20个字符之间");
    }
}
