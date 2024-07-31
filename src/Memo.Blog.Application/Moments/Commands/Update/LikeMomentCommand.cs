namespace Memo.Blog.Application.Moments.Commands.Update;

public record LikeMomentCommand(long MomentId) : IRequest<Result>;

public class LikeMomentCommandValidator : AbstractValidator<LikeMomentCommand>
{
    public LikeMomentCommandValidator()
    {
        RuleFor(x => x.MomentId)
            .Must(x => x > 0)
            .WithMessage("动态Id必须大于0");
    }
}
