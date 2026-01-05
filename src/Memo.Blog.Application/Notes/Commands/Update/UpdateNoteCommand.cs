namespace Memo.Blog.Application.Notes.Commands.Update;

[Authorize(Permissions = ApiPermission.Note.Update)]
public record UpdateNoteCommand(long NoteId, long CatalogId, string Title, string Content) : IAuthorizeableRequest<Result>;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    { 
        RuleFor(x => x.NoteId)
            .NotEmpty()
            .WithMessage("笔记Id不能为空");
        RuleFor(x => x.CatalogId)
            .NotEmpty()
            .WithMessage("笔记目录Id不能为空");
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("笔记标题不能为空")
            .MaximumLength(200)
            .WithMessage("笔记标题不能超过200个字符");
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("笔记内容不能为空");
    }
}
