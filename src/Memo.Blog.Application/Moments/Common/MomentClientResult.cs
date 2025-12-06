namespace Memo.Blog.Application.Moments.Common;

internal record MomentClientResult : MomentResult
{
    /// <summary>
    /// 是否已点过赞
    /// </summary>
    public bool IsLike { get; set; }
}
