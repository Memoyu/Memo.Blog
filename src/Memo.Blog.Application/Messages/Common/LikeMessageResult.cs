using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Messages.Common;

public record LikeMessageResult
{
    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    public long BelongId { get; set; }

    /// <summary>
    /// 所属类型
    /// </summary>
    public BelongType LikeType { get; set; }
}
