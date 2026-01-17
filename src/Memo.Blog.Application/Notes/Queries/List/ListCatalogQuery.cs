namespace Memo.Blog.Application.Notes.Queries.List;

[Authorize(Permissions = ApiPermission.Note.List)]
public record ListCatalogQuery : IAuthorizeableRequest<Result>;
