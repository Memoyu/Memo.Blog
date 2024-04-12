namespace Memo.Blog.Application.Tags.Queries.Anlyanis;

/// <summary>
/// 关联文章数据汇总
/// </summary>
[Authorize(Permissions = ApiPermission.Tag.RelationSummary)]
public record RelationSummaryQuery() : IAuthorizeableRequest<Result>;
