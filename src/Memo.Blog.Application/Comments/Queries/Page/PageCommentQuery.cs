using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Queries.Page;

[Authorize(Permissions = ApiPermission.Comment.Page)]
public record PageCommentQuery : PaginationQuery, IRequest<Result>
{
    public CommentType CommentType { get; set; }

    public string? Nickname { get; set; }

    public string? Ip { get; set; }

    public DateTime? CommentTimeBegin { get; set; }

    public DateTime? CommentTimeEnd { get; set; }
}
