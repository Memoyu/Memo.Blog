namespace Memo.Blog.Domain.Enums;

/// <summary>
/// 评论类型
/// </summary>
public enum BelongType
{
    [Description("文章")]
    Article = 0,

    [Description("动态")]
    Moment = 1,

    [Description("关于")]
    About = 2,
}
