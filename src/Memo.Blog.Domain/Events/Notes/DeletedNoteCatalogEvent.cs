namespace Memo.Blog.Domain.Events.Notes;

public record DeletedNoteGroupEvent(long groupId) : IDomainEvent;

