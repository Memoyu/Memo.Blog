namespace Memo.Blog.Domain.Events.Articles;

public record DeletedArticleEvent(long ArticleId) : IDomainEvent;
