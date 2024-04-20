namespace Memo.Blog.Application.OpenSources.Queries.Page;

[Authorize(Permissions = ApiPermission.OpenSource.GitHubRepoPage)]
public record PageGitHubRepoQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
    public string? KeyWord { get; set; }
}

public class PageGitHubRepoQueryValidator : AbstractValidator<PageGitHubRepoQuery>
{
    public PageGitHubRepoQueryValidator()
    {
    }
}
