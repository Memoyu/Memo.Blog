using Memo.Blog.Domain.Events.Roles;

namespace Memo.Blog.Application.Roles.Events;

public class RoleDeletedEventHadler(
    IBaseDefaultRepository<RolePermission> rolePermissionRepo,
    IBaseDefaultRepository<UserRole> userRoleRepo
    ) : INotificationHandler<RoleDeleteEvent>
{
    public async Task Handle(RoleDeleteEvent notification, CancellationToken cancellationToken)
    {
        // 删除关联权限
        var permissions = await rolePermissionRepo.DeleteAsync(rp => rp.RoleId == notification.RoleId, cancellationToken);

        // 删除用户关联
        var userRoles = await userRoleRepo.DeleteAsync(ur => ur.RoleId == notification.RoleId, cancellationToken);
    }
}
