namespace Memo.Blog.Domain.Events.Articles;

public record DeletedArticleCommentEvent(long ArticleId, long CommentId) : IDomainEvent;
