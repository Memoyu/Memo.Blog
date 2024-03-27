namespace Memo.Blog.Domain.Events.Articles;

public  record ArticleUpdateTagEvent(long ArticleId) : IDomainEvent;
