using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Comments.Common;

public record CommentResult
{
    /// <summary>
    /// 评论Id
    /// </summary>
    public long CommentId { get; set; }

    /// <summary>
    /// 父评论Id
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    public long BelongId { get; set; }

    /// <summary>
    /// 评论类型
    /// </summary>
    public CommentType CommentType { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    ///  头像url
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 头像来源类型
    /// </summary>
    public AvatarOriginType AvatarOriginType { get; set; }

    /// <summary>
    /// 头像来源
    /// </summary>
    public string AvatarOrigin { get; set; } = string.Empty;

    /// <summary>
    /// 评论所在IP
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 评论IP所属
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 是否展示
    /// </summary>
    public bool Showable { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
