using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Messages.Common;

public record CommentMessageContent
{
    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    public long BelongId { get; set; }

    /// <summary>
    /// 所属类型
    /// </summary>
    public BelongType CommentType { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
