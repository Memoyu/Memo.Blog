namespace Memo.Blog.Application.Messages.Queries.Page;

[Authorize(Permissions = ApiPermission.Message.Page)]
public record PageMessageQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
    public string? KeyWord { get; set; }
}

public class PageMessageQueryValidator : AbstractValidator<PageMessageQuery>
{
    public PageMessageQueryValidator()
    {
    }
}
