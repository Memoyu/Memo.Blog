using System.Reflection;
using Memo.Blog.Domain.Events.Permissions;

namespace Memo.Blog.Application.Permissions.Events;

public class PermissionSyncEventHandler : INotificationHandler<PermissionSyncEvent>
{
    public Task Handle(PermissionSyncEvent notification, CancellationToken cancellationToken)
    {
        var permissions = new List<Permission>();
        var apiPermissionTypes = typeof(ApiPermission).GetNestedTypes().ToList();
        foreach ( var apiPermissionType in apiPermissionTypes)
        {
            var permissionConst = apiPermissionType.GetFields();

        }
        var s = typeof(ApiPermission.About).GetFields();

        return Task.CompletedTask;
    }
}
