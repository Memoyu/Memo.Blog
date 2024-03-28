namespace Memo.Blog.Domain.Events.Articles;

public  record UpdatedArticleTagEvent(long TagId) : IDomainEvent;
