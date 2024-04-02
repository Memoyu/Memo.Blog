namespace Memo.Blog.Application.Moments.Commands.Create;

[Authorize(Permissions = ApiPermission.Moment.Create)]
public record CreateMomentCommand(
    List<string> Tags,
    string Content,
    bool Showable,
    bool Commentable
    ) : IAuthorizeableRequest<Result>;

public class CreateMomentCommandValidator : AbstractValidator<CreateMomentCommand>
{
    public CreateMomentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("动态内容不能为空");
    }
}
