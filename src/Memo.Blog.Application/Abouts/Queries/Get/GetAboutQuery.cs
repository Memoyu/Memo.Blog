namespace Memo.Blog.Application.Abouts.Queries.Get;

[Authorize(Permissions = ApiPermission.About.Get)]
public record GetAboutQuery() : IRequest<Result>;


public class GetAboutQueryValidator : AbstractValidator<GetAboutQuery>
{
    public GetAboutQueryValidator()
    {
    }
}

