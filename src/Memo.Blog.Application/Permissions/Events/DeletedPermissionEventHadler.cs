using Memo.Blog.Domain.Events.Permissions;

namespace Memo.Blog.Application.Permissions.Events;

public class DeletedPermissionEventHadler(
    IBaseDefaultRepository<RolePermission> rolePermissionRepo
    ) : INotificationHandler<DeletedPermissionEvent>
{
    public async Task Handle(DeletedPermissionEvent notification, CancellationToken cancellationToken)
    {
        // 删除关联权限
        var permissionAffrows = await rolePermissionRepo.DeleteAsync(rp => rp.PermissionId == notification.PermissionId, cancellationToken);
    }
}
