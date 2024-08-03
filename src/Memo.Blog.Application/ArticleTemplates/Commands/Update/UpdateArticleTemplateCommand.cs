namespace Memo.Blog.Application.ArticleTemplates.Commands.Update;

[Authorize(Permissions = ApiPermission.ArticleTemplate.Update)]
public record UpdateArticleTemplateCommand(long TemplateId, string Name, string? Content) : IAuthorizeableRequest<Result>;

public class UpdateArticleTemplateCommandValidator : AbstractValidator<UpdateArticleTemplateCommand>
{
    public UpdateArticleTemplateCommandValidator()
    {
        RuleFor(x => x.TemplateId)
            .Must(x => x > 0)
            .WithMessage("分类Id必须大于0");

        RuleFor(x => x.Name)
                  .NotEmpty()
                  .WithMessage("文章模板名称不能为空");

        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(20)
            .WithMessage("分类名称长度在1-20个字符之间");
    }
}

