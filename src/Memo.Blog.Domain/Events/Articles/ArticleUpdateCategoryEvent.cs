namespace Memo.Blog.Domain.Events.Articles;

public record ArticleUpdateCategoryEvent(long CategoryId, long NewCategoryId) : IDomainEvent;
