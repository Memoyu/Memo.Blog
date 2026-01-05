namespace Memo.Blog.Application.Notes.Commands.Update;

[Authorize(Permissions = ApiPermission.Note.Update)]
public record UpdateCatalogCommand(long CatalogId, string Title) : IAuthorizeableRequest<Result>;

public class UpdateCatalogCommandValidator : AbstractValidator<UpdateCatalogCommand>
{
    public UpdateCatalogCommandValidator()
    { 
        RuleFor(x => x.CatalogId)
            .NotEmpty()
            .WithMessage("目录Id不能为空");
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("目录标题不能为空")
            .MaximumLength(200)
            .WithMessage("目录标题不能超过200个字符");
    }
}
