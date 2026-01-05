namespace Memo.Blog.Application.Notes.Commands.Delete;

[Authorize(Permissions = ApiPermission.Note.Delete)]
public record DeleteNoteCommand(long NoteId) : IAuthorizeableRequest<Result>;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty()
            .WithMessage("笔记Id不能为空");
    }
}
