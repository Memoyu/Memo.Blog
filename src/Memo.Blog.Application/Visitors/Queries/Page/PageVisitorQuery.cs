namespace Memo.Blog.Application.Visitors.Queries.Page;

[Authorize(Permissions = ApiPermission.Visitor.Page)]
public record PageVisitorQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
    public long? VisitorId { get; set; }

    public string? Nickname { get; set; }

    public string? Region { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }
}
