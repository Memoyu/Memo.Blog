namespace Memo.Blog.Domain.Events.Tags;

public record DeletedTagEvent(long TagId) : IDomainEvent;
