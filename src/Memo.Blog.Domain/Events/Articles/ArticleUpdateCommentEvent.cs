namespace Memo.Blog.Domain.Events.Articles;

public record ArticleUpdateCommentEvent(long ArticleId) : IDomainEvent;
