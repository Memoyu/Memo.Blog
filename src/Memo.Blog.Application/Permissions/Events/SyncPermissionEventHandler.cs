using System.ComponentModel;
using System.Reflection;
using Memo.Blog.Domain.Events.Permissions;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Permissions.Events;

public class SyncPermissionEventHandler(
    ILogger<SyncPermissionEventHandler> logger,
    IBaseDefaultRepository<Permission> permissionRepo
    ) : INotificationHandler<SyncPermissionEvent>
{
    public async Task Handle(SyncPermissionEvent notification, CancellationToken cancellationToken)
    {
        var permissions = new List<Permission>();
        var permissionTypes = typeof(ApiPermission).GetNestedTypes().ToList();
        foreach (var permissionType in permissionTypes)
        {
            var permissionDesc = permissionType.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()
                ?? throw new Exception($"{permissionType.FullName} 未添加Description特性");
            var module = permissionType.Name;

            var permissionFields = permissionType.GetFields();
            foreach (var fieldInfo in permissionFields)
            {
                var fieldDesc = fieldInfo.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()
                    ?? throw new Exception($"{permissionType.FullName}下的{fieldInfo.Name} 未添加Description特性");

                var permission = new Permission
                {
                    Module = module,
                    ModuleName = permissionDesc.Description,
                    Name = fieldDesc.Description,
                    Signature = fieldInfo.GetValue(null)!.ToString()!
                };

                permissions.Add(permission);
            }
        }

        var inserts = new List<Permission>();
        var updates = new List<Permission>();
        var dbPermissions = await permissionRepo.Select.ToListAsync(cancellationToken);
        foreach (var permission in permissions)
        {
            var dbPermission = dbPermissions.FirstOrDefault(p => permission.Module == p.Module && permission.Signature == p.Signature);
            if (dbPermission is null)
            {
                inserts.Add(permission);
            }
            else
            {
                if (dbPermission.ModuleName != permission.ModuleName || dbPermission.Name != permission.Name)
                {
                    permission.Id = dbPermission.Id;
                    permission.PermissionId = dbPermission.PermissionId;
                    updates.Add(permission);
                }
                dbPermissions.Remove(dbPermission);
            }
        }

        // 增加权限删除事件
        var deletes = dbPermissions.Select(p =>
        {
            p.AddDomainEvent(new DeletedPermissionEvent(p.PermissionId));
            return p;
        }).ToList();

        var insertCount = (await permissionRepo.InsertAsync(inserts, cancellationToken))?.Count ?? 0;
        var updateCount = await permissionRepo.UpdateAsync(updates, cancellationToken);
        var deleteCount = await permissionRepo.DeleteAsync(deletes, cancellationToken);

        logger.LogInformation($"权限数据同步：[新增权限]：{inserts.Count}-{insertCount}条；[更新权限]：{updates.Count}-{updateCount}条；[删除权限]：{deletes.Count}-{deleteCount}条；");
    }
}
