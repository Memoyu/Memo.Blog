using Memo.Blog.Application.Roles.Common;
using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Users.Queries.Get;

public class GetUserQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<UserRole> userRoleRepo
    ) : IRequestHandler<GetUserQuery, Result>
{
    public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.Select.Where(u => u.UserId == request.UserId).FirstAsync(cancellationToken) ?? throw new ApplicationException("用户不存在");
        var userRoles = await userRoleRepo.Select
            .Include(u => u.Role)
            .Where(u => u.UserId == request.UserId).ToListAsync(cancellationToken) ?? [];

        var dto = mapper.Map<UserWithRoleResult>(user);
        dto.Roles = userRoles.Select(ur => mapper.Map<RoleResult>(ur.Role)).ToList();

        return Result.Success(dto);
    }
}
