namespace Memo.Blog.Application.Users.Queries.Page;

[Authorize(Permissions = ApiPermission.User.Page)]
public record PageUserQuery : PaginationQuery, IRequest<Result>
{
    public long? UserId { get; set; }

    public string? Username { get; set; }

    public string? Nickname { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

}
