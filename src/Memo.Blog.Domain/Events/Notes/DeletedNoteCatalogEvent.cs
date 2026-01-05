namespace Memo.Blog.Domain.Events.Notes;

public record DeletedNoteCatalogEvent(long catalogId) : IDomainEvent;

