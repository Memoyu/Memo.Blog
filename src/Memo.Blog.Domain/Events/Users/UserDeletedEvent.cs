namespace Memo.Blog.Domain.Events.Users;

public record UserDeletedEvent(long UserId) : IDomainEvent;
