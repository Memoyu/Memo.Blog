namespace Memo.Blog.Application.Notes.Commands.Update;

[Authorize(Permissions = ApiPermission.Note.Update)]
public record UpdateNoteCatalogCommand(long NoteId, long CatalogId) : IAuthorizeableRequest<Result>;

public class UpdateNoteCatalogCommandValidator : AbstractValidator<UpdateNoteCatalogCommand>
{
    public UpdateNoteCatalogCommandValidator()
    { 
        RuleFor(x => x.NoteId)
            .NotEmpty()
            .WithMessage("笔记Id不能为空");
        RuleFor(x => x.CatalogId)
            .NotEmpty()
            .WithMessage("目录Id不能为空");
    }
}
