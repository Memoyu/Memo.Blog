namespace Memo.Blog.Application.Notes.Commands.Create;

[Authorize(Permissions = ApiPermission.Note.Create)]
public record CreateNoteCommand(long? GroupId, string Title, string? Content) : IAuthorizeableRequest<Result>;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("笔记标题不能为空")
            .MaximumLength(200)
            .WithMessage("笔记标题不能超过200个字符");
    }
}
