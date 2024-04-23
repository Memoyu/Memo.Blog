using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Queries.Page;

[Authorize(Permissions = ApiPermission.Comment.Page)]
public record PageCommentQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
    public CommentType? CommentType { get; set; }

    public string? Nickname { get; set; }

    public string? Ip { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }
}

public record PageCommentClientQuery : PaginationQuery, IRequest<Result>
{
    public CommentType CommentType { get; set; }

    public long? BelongId { get; set; }
}

