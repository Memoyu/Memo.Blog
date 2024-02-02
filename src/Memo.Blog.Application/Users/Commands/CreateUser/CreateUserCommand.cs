using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Users.Commands.CreateUser;

[Authorize(Permissions = Permissions.User.Create)]
[Transactional]
public record CreateUserCommand(
    string Username,
    string Nickname,
    string Password,
    string Avatar,
    string PhoneNumber,
    string Email
    ) : IRequest<Result<UserResult>>;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .MinimumLength(1)
            .MaximumLength(50);

        RuleFor(x => x.Nickname)
           .MinimumLength(2)
           .MaximumLength(50);

        RuleFor(x => x.Password)
            .MinimumLength(4)
            .MaximumLength(20);

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
