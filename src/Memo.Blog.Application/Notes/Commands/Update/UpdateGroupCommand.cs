namespace Memo.Blog.Application.Notes.Commands.Update;

[Authorize(Permissions = ApiPermission.Note.Update)]
public record UpdateGroupCommand(long GroupId, string Title) : IAuthorizeableRequest<Result>;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    { 
        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("分组Id不能为空");
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("分组标题不能为空")
            .MaximumLength(200)
            .WithMessage("分组标题不能超过200个字符");
    }
}
