namespace Memo.Blog.Domain.Enums;

public enum VisitLogBehavior
{
    [Description("文章")]
    Article = 0,

    [Description("动态")]
    Moment = 1,

    [Description("友链")]
    Friend = 2,

    [Description("工具")]
    Tool = 3,

    [Description("关于")]
    About = 4,
}
