namespace Memo.Blog.Domain.Events.Permissions;

public record DeletedPermissionEvent(long PermissionId) : IDomainEvent;
