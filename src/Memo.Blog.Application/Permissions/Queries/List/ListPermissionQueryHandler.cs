using Memo.Blog.Application.Permissions.Common;
using Memo.Blog.Application.Roles.Common;

namespace Memo.Blog.Application.Permissions.Queries.List;

public class ListPermissionQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Permission> permissionResp,
    IBaseDefaultRepository<RolePermission> rolePermissionResp
    ) : IRequestHandler<ListPermissionQuery, Result>
{
    public async Task<Result> Handle(ListPermissionQuery request, CancellationToken cancellationToken)
    {
        var permissions = await permissionResp.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), p => p.Name.Contains(request.Name!))
            .ToListAsync(cancellationToken);

        var rolePermissions = await rolePermissionResp.Select
            .Include(r => r.Role).ToListAsync(cancellationToken);

        var dtos = mapper.Map<List<PermissionResult>>(permissions);
        foreach (var d in dtos)
        {
            d.Roles = rolePermissions.Where(r => r.PermissionId == d.PermissionId).Select(r => mapper.Map<RoleResult>(r)).ToList();
        }

        return Result.Success(dtos);
    }
}
