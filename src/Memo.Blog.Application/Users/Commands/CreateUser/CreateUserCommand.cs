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
    string Email,
    List<long> Roles
    ) : IRequest<Result<UserResult>>;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(
        IBaseDefaultRepository<Role> roleResp,
        IBaseDefaultRepository<User> userResp)
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(50);

        RuleFor(x => x.Username)
            .MustAsync(async (x, ct) => !await userResp.Select.AnyAsync(u => x == u.Username, ct))
            .WithMessage("用户名称已存在");

        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches("^[A-Za-z0-9_*&$#@]{4,20}$")
            .WithMessage("密码长度必须在6~22位之间，包含字符、数字和 _");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Roles)
            .NotEmpty()
            .WithMessage("角色不能为空");

        RuleFor(x => x.Roles)
           .MustAsync(async (x, ct) => await roleResp.Select.AnyAsync(r => x.Contains(r.RoleId), ct))
           .WithMessage("角色不存在");
    }
}
