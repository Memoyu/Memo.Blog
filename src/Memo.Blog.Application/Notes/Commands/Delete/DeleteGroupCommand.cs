namespace Memo.Blog.Application.Notes.Commands.Delete;

[Authorize(Permissions = ApiPermission.Note.DeleteGroup)]
public record DeleteGroupCommand(long GroupId) : IAuthorizeableRequest<Result>;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("分组Id不能为空");
    }
}
