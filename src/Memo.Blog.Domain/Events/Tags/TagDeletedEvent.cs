namespace Memo.Blog.Domain.Events.Tags;

public record TagDeletedEvent(long TagId) : IDomainEvent;
