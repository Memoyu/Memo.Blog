using Memo.Blog.Application.Permissions.Common;
using Memo.Blog.Application.Roles.Common;

namespace Memo.Blog.Application.Permissions.Queries.List;

public class ListPermissionQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Permission> permissionRepo,
    IBaseDefaultRepository<RolePermission> rolePermissionRepo
    ) : IRequestHandler<ListPermissionQuery, Result>
{
    public async Task<Result> Handle(ListPermissionQuery request, CancellationToken cancellationToken)
    {
        var permissions = await permissionRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), p => p.Name.Contains(request.Name!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Signature), p => p.Signature.Contains(request.Signature!))
            .ToListAsync(cancellationToken);

        var rolePermissions = await rolePermissionRepo.Select
            .Include(r => r.Role).ToListAsync(cancellationToken);

        var dtos = mapper.Map<List<PermissionResult>>(permissions);
        foreach (var d in dtos)
        {
            d.Roles = rolePermissions.Where(r => r.PermissionId == d.PermissionId).Select(mapper.Map<RoleResult>).ToList();
        }

        return Result.Success(dtos);
    }
}
