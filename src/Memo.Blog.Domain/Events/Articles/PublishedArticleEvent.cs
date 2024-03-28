namespace Memo.Blog.Domain.Events.Articles;

public record PublishedArticleEvent(long ArticleId) : IDomainEvent;
