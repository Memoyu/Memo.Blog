namespace Memo.Blog.Domain.Events.Roles;

public record RoleDeleteEvent(long RoleId) : IDomainEvent;
