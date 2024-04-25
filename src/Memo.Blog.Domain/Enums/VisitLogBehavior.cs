namespace Memo.Blog.Domain.Enums;

public enum VisitLogBehavior
{
    [Description("首页")]
    Home = 0,

    [Description("文章列表")]
    Article = 1,

    [Description("文章详情")]
    ArticleDetail = 11,

    [Description("实验室")]
    Labs = 2,

    [Description("动态")]
    Moment = 3,

    [Description("关于")]
    About = 4,
}
