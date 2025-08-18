namespace Memo.Blog.Application.Tokens.Commands.Generate;

public record GenerateTokenCommand(string Username, string Password) : IRequest<Result>;

public class GenerateTokenValidator : AbstractValidator<GenerateTokenCommand>
{
    public GenerateTokenValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("用户名不能为空");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("密码不能为空");
    }
}

