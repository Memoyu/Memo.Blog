namespace Memo.Blog.Domain.Events.Articles;

public record UpdatedArticleCommentEvent(long ArticleId) : IDomainEvent;
