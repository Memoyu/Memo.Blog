namespace Memo.Blog.Application.Articles.Queries.Get;

public record SearchArticleQuery(string KeyWord) :PaginationQuery, IRequest<Result>;
