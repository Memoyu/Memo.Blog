namespace Memo.Blog.Application.Friends.Queries.Page;

[Authorize(Permissions = ApiPermission.Friend.Page)]
public record PageFriendQuery : PaginationQuery, IRequest<Result>
{
    public string? Nickname { get; set; }

    public string? Description { get; set; }

    public string? Site { get; set; }

}
