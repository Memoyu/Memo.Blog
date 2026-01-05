namespace Memo.Blog.Application.Notes.Commands.Delete;

[Authorize(Permissions = ApiPermission.Note.DeleteCatalog)]
public record DeleteCatalogCommand(long CatalogId) : IAuthorizeableRequest<Result>;

public class DeleteCatalogCommandValidator : AbstractValidator<DeleteCatalogCommand>
{
    public DeleteCatalogCommandValidator()
    {
        RuleFor(x => x.CatalogId)
            .NotEmpty()
            .WithMessage("目录Id不能为空");

        RuleFor(x => x.CatalogId)
            .Equal(InitConst.InitNoteCatalogId)
            .WithMessage("默认目录，无法删除");
    }
}
