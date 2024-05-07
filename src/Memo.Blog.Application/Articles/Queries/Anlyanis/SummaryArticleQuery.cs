namespace Memo.Blog.Application.Articles.Queries.Anlyanis;

[Authorize(Permissions = ApiPermission.Article.Summary)]
public record SummaryArticleQuery() : IAuthorizeableRequest<Result>;

public record SummaryArticleClientQuery() : IRequest<Result>;
