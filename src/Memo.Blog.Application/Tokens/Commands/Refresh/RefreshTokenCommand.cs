namespace Memo.Blog.Application.Tokens.Commands.Refresh;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Result>;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("刷新Token不能为空");
    }
}
