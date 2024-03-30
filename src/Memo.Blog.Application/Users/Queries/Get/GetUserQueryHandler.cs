using Memo.Blog.Application.Roles.Common;
using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Users.Queries.Get;

public class GetUserQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<UserRole> userRoleRepo,
     IBaseDefaultRepository<UserIdentity> userIdentityRepo
    ) : IRequestHandler<GetUserQuery, Result>
{
    public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.Select.Where(u => u.UserId == request.UserId).FirstAsync(cancellationToken) ?? throw new ApplicationException("用户不存在");
        var userRoles = await userRoleRepo.Select
            .Include(u => u.Role)
            .Where(u => u.UserId == request.UserId).ToListAsync(cancellationToken) ?? [];

        var userIdentity = await userIdentityRepo.Select.Where(ui => ui.UserId == request.UserId).FirstAsync(cancellationToken);
        
        var dto = mapper.Map<UserWithUserIdentityResult>(user);
        dto.Roles = userRoles.Select(ur => mapper.Map<RoleListResult>(ur.Role)).ToList();
        dto.UserIdentity = mapper.Map<UserIdentityResult>(userIdentity);

        return Result.Success(dto);
    }
}
