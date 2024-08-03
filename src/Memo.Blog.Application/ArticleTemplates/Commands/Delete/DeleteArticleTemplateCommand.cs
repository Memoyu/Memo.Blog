namespace Memo.Blog.Application.ArticleTemplates.Commands.Delete;

[Authorize(Permissions = ApiPermission.ArticleTemplate.Delete)]
public record DeleteArticleTemplateCommand(long TemplateId) : IAuthorizeableRequest<Result>;

public class DeleteArticleTemplateCommandValidator : AbstractValidator<DeleteArticleTemplateCommand>
{
    public DeleteArticleTemplateCommandValidator()
    {
        RuleFor(x => x.TemplateId)
            .Must(x => x > 0)
            .WithMessage("模板Id必须大于0");
    }
}

