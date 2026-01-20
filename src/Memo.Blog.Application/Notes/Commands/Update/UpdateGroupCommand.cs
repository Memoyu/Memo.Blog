namespace Memo.Blog.Application.Notes.Commands.Update;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Type">类型：group： 0， note： 1</param>
/// <param name="GroupId"></param>
[Authorize(Permissions = ApiPermission.Note.UpdateGroup)]
public record UpdateGroupCommand(long Id, int Type, long? GroupId) : IAuthorizeableRequest<Result>;

public class UpdateNoteGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateNoteGroupCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id不能为空");
    }
}
