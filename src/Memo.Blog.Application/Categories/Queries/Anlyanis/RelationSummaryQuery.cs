namespace Memo.Blog.Application.Categories.Queries.Anlyanis;

/// <summary>
/// 关联文章数据汇总
/// </summary>
[Authorize(Permissions = ApiPermission.Category.RelationSummary)]
public record RelationSummaryQuery() : IAuthorizeableRequest<Result>;
