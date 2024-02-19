using Memo.Blog.Application.Users.Common;
using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Users.Commands.Create;

public class CreateUserCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<User> userResp,
    IBaseDefaultRepository<UserIdentity> userIdentityResp,
    IBaseDefaultRepository<UserRole> userRoleResp
    ) : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // 用户信息
        var user = mapper.Map<User>(request);
        var userId = SnowFlakeUtil.NextId();
        user.UserId = userId;
        user = await userResp.InsertAsync(user);

        // 用户身份认
        var identity = new UserIdentity
        {
            UserId = user.UserId,
            IdentityType = UserIdentityType.Password,
            Identifier = user.PhoneNumber,
            Credential = EncryptUtil.Encrypt(request.Password)
        };
        await userIdentityResp.InsertAsync(identity);

        // 用户角色
        var userRoles = new List<UserRole>();
        request.Roles.ForEach(id => userRoles.Add(new UserRole
        {
            UserId = user.UserId,
            RoleId = id
        }));
        await userRoleResp.InsertAsync(userRoles);

        var result = mapper.Map<UserResult>(user);

        return Result.Success(result);
    }
}
