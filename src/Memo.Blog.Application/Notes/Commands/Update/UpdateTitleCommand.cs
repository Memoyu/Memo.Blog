namespace Memo.Blog.Application.Notes.Commands.Update;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Type">类型：group： 0， note： 1</param>
/// <param name="Title"></param>
[Authorize(Permissions = ApiPermission.Note.UpdateTitle)]
public record UpdateTitleCommand(long Id, int Type, string Title) : IAuthorizeableRequest<Result>;

public class UpdateTitleCommandValidator : AbstractValidator<UpdateTitleCommand>
{
    public UpdateTitleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id不能为空");
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("分组标题不能为空")
            .MaximumLength(200)
            .WithMessage("分组标题不能超过200个字符");
    }
}
