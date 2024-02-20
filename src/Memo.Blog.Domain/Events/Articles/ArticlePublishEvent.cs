namespace Memo.Blog.Domain.Events.Articles;

public record ArticlePublishEvent(long ArticleId) : IDomainEvent;
