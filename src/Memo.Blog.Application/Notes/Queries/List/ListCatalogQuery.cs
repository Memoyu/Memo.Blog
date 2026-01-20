namespace Memo.Blog.Application.Notes.Queries.List;

/// <summary>
/// 
/// </summary>
/// <param name="OnlyGroup">只加载分组</param>
[Authorize(Permissions = ApiPermission.Note.Catalog)]
public record ListCatalogQuery(bool? OnlyGroup) : IAuthorizeableRequest<Result>;
