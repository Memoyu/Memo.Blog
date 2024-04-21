namespace Memo.Blog.Application.OpenSources.Queries.List;

[Authorize(Permissions = ApiPermission.OpenSource.List)]
public record ListProjectQuery : IAuthorizeableRequest<Result>
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }

}

public class ListProjectQueryValidator : AbstractValidator<ListProjectQuery>
{
    public ListProjectQueryValidator()
    {
    }
}

public record ListProjectClientQuery() : IRequest<Result>;
