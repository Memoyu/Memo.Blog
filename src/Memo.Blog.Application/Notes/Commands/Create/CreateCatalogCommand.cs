namespace Memo.Blog.Application.Notes.Commands.Create;

[Authorize(Permissions = ApiPermission.Note.CreateCatalog)]
public record CreateCatalogCommand(long? ParentId, string Title) : IAuthorizeableRequest<Result>;

public class CreateCatalogCommandValidator : AbstractValidator<CreateCatalogCommand>
{
    public CreateCatalogCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("目录标题不能为空")
            .MaximumLength(200)
            .WithMessage("目录标题不能超过200个字符");
    }
}
