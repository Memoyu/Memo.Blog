namespace Memo.Blog.Domain.Events.Articles;

public  record ArticleDeleteTagEvent(long ArticleId, long TagId) : IDomainEvent;
