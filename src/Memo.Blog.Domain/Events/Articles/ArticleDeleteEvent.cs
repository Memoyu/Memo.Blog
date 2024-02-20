namespace Memo.Blog.Domain.Events.Articles;

public record ArticleDeleteEvent(long ArticleId) : IDomainEvent;
