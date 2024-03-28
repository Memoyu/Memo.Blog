﻿namespace Memo.Blog.Application.Users.Commands.Create;

[Authorize(Permissions = ApiPermission.User.Create)]
[Transactional]
public record CreateUserCommand(
    string Username,
    string Nickname,
    string Password,
    string? Avatar,
    string? PhoneNumber,
    string? Email,
    List<long> Roles
    ) : IRequest<Result>;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(
        IBaseDefaultRepository<Role> roleRepo,
        IBaseDefaultRepository<User> userRepo)
    {
        RuleFor(x => x.Username)
            .MinimumLength(1)
            .MaximumLength(50)
            .WithMessage("用户名称长度在1-50个字符之间");

        RuleFor(x => x.Username)
            .MustAsync(async (x, ct) => !await userRepo.Select.AnyAsync(u => x == u.Username, ct))
            .WithMessage("用户名称已存在");

        RuleFor(x => x.Nickname)
            .MinimumLength(1)
            .MaximumLength(50)
            .WithMessage("用户昵称长度在1-50个字符之间");

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches("^[A-Za-z0-9_*&$#@]{4,20}$")
            .WithMessage("密码长度必须在6~22位之间，包含字符、数字和 _");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("邮箱格式有误")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Roles)
            .NotEmpty()
            .WithMessage("角色不能为空");

        RuleForEach(x => x.Roles)
           .MustAsync(async (x, ct) => x > 0 && await roleRepo.Select.AnyAsync(r => x == r.RoleId, ct))
           .WithMessage("角色不存在");
    }
}
