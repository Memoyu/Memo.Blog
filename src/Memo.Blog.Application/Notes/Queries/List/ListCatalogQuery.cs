namespace Memo.Blog.Application.Notes.Queries.List;

[Authorize(Permissions = ApiPermission.Note.Catalog)]
public record ListCatalogQuery : IAuthorizeableRequest<Result>;
