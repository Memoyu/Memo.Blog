namespace Memo.Blog.Application.Notes.Commands.Update;

[Authorize(Permissions = ApiPermission.Note.Update)]
public record UpdateNoteGroupCommand(long NoteId, long? GroupId) : IAuthorizeableRequest<Result>;

public class UpdateNoteGroupCommandValidator : AbstractValidator<UpdateNoteGroupCommand>
{
    public UpdateNoteGroupCommandValidator()
    { 
        RuleFor(x => x.NoteId)
            .NotEmpty()
            .WithMessage("笔记Id不能为空");
    }
}
