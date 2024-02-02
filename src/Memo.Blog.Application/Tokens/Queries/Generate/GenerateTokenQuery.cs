namespace Memo.Blog.Application.Tokens.Queries.Generate;

public record GenerateTokenQuery(string Username, string Password) : IRequest<Result<GenerateTokenResult>>;

public class GenerateTokenQueryValidator : AbstractValidator<GenerateTokenQuery>
{
    public GenerateTokenQueryValidator(
        IBaseDefaultRepository<User> userResp,
        IBaseDefaultRepository<UserIdentity> userIdentityResp)
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("用户名不能为空");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("密码不能为空");
    }
}

