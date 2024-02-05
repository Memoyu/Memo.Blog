using Memo.Blog.Application.Users.Common;
using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<User> _userResp,
    IBaseDefaultRepository<UserIdentity> _userIdentityResp,
    IBaseDefaultRepository<UserRole> _userRoleResp
    ) : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // 用户信息
        var user = _mapper.Map<User>(request);
        var userId = SnowFlakeUtil.NextId();
        user.UserId = userId;
        user = await _userResp.InsertAsync(user);

        // 用户身份认
        var identity = new UserIdentity
        {
            UserId = user.UserId,
            IdentityType = UserIdentityType.Password,
            Identifier = user.PhoneNumber,
            Credential = EncryptUtil.Encrypt(request.Password)
        };
        await _userIdentityResp.InsertAsync(identity);

        // 用户角色
        var userRoles = new List<UserRole>();
        request.Roles.ForEach(id => userRoles.Add(new UserRole
        {
            UserId = user.UserId,
            RoleId = id
        }));
        await _userRoleResp.InsertAsync(userRoles);

        var result = _mapper.Map<UserResult>(user);

        return Result.Success(result);
    }
}
