using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Common;

public record CommentResult (
    long CommentId,
    long ParentId,
    long BelongId, 
    CommentType CommentType, 
    string Nickname,
    string Email,
    string Content,
    string Avatar,
    AvatarOriginType AvatarOriginType,
    string AvatarOrigin,
    string Ip,
    bool Showable
);
