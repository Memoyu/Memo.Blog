namespace Memo.Blog.Domain.Events.Articles;

public record ArticleDeleteCommentEvent(long ArticleId, long CommentId) : IDomainEvent;
