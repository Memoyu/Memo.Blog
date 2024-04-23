namespace Memo.Blog.Application.Abouts.Queries.Get;

[Authorize(Permissions = ApiPermission.About.Get)]
public record GetAboutQuery() : IAuthorizeableRequest<Result>;


public class GetAboutQueryValidator : AbstractValidator<GetAboutQuery>
{
    public GetAboutQueryValidator()
    {
    }
}

public record GetAboutClientQuery() : IRequest<Result>;

