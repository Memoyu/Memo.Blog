using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Password,
    string Email
    ) : IRequest<Result<UserResult>>;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .MinimumLength(2)
            .MaximumLength(10000);

        RuleFor(x => x.Password)
            .MinimumLength(2)
            .MaximumLength(10000);

        RuleFor(x => x.Email).EmailAddress();
    }
}
