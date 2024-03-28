namespace Memo.Blog.Domain.Events.Users;

public record DeletedUserEvent(long UserId) : IDomainEvent;
