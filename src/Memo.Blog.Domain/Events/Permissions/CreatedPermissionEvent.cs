namespace Memo.Blog.Domain.Events.Permissions;

public record CreatedPermissionEvent(long PermissionId) : IDomainEvent;
