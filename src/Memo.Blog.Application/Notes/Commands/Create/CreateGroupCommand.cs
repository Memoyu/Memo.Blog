namespace Memo.Blog.Application.Notes.Commands.Create;

[Authorize(Permissions = ApiPermission.Note.CreateGroup)]
public record CreateGroupCommand(long? ParentId, string Title) : IAuthorizeableRequest<Result>;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("分组标题不能为空")
            .MaximumLength(200)
            .WithMessage("分组标题不能超过200个字符");
    }
}
