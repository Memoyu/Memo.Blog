using Memo.Blog.Domain.Entities;

namespace Memo.Blog.Domain.Events.Articles;

public record UpdatedArticleCategoryEvent(Category Category) : IDomainEvent;
