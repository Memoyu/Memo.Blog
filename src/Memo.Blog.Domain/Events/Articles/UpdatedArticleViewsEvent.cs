namespace Memo.Blog.Domain.Events.Articles;

public record UpdatedArticleViewsEvent(long ArticleId, long VisitorId) : IDomainEvent;

