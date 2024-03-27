namespace Memo.Blog.Application.Roles.Commands.Create;

public class CreateRoleCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Role> roleResp,
    IBaseDefaultRepository<Permission> permissionResp,
    IBaseDefaultRepository<RolePermission> rolePermissionResp
    ) : IRequestHandler<CreateRoleCommand, Result>
{
    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var exist = await roleResp.Select.AnyAsync(r => r.Name == request.Name, cancellationToken);
        if (exist) throw new ApplicationException("角色已存在");

        var permission = await permissionResp.Select.Where(p => request.Permissions.Contains(p.PermissionId)).ToListAsync(cancellationToken);
        foreach (var permissionId in request.Permissions)
        {
            if (!permission.Any(t => t.PermissionId == permissionId)) throw new ApplicationException($"{permissionId}权限不存在");
        }

        var role = mapper.Map<Role>(request);
        role = await roleResp.InsertAsync(role, cancellationToken);
        if (role.Id <= 0) throw new ApplicationException("保存角色失败");

        var rolePermissions = request.Permissions.Select(p => new RolePermission { RoleId = role.RoleId, PermissionId = p }).ToList();
        await rolePermissionResp.InsertAsync(rolePermissions, cancellationToken);

        return Result.Success(role.RoleId);
    }
}
