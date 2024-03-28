namespace Memo.Blog.Domain.Events.Categories;

public record DeletedCategoryEvent(long CategoryId) : IDomainEvent;
