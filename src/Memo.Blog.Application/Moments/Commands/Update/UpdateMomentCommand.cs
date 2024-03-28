using Memo.Blog.Application.Moments.Commands.Create;

namespace Memo.Blog.Application.Moments.Commands.Update;

[Authorize(Permissions = ApiPermission.Moment.Update)]
public record UpdateMomentCommand(
    long MomentId,
    List<string> Tags,
    string Content,
    bool Showable,
    bool Commentable
    ) : IRequest<Result>;

public class UpdateMomentCommandValidator : AbstractValidator<UpdateMomentCommand>
{
    public UpdateMomentCommandValidator()
    {
        RuleFor(x => x.MomentId)
          .Must(x => x > 0)
          .WithMessage("动态Id必须大于0");


        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("动态内容不能为空");
    }
}
