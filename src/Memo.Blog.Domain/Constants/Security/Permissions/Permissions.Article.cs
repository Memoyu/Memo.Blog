namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("文章")]
    public static class Article
    {
        [Description("创建文章")]
        public const string Create = "create:article";

        [Description("更新文章")]
        public const string Update = "update:article";

        [Description("删除文章")]
        public const string Delete = "delete:article";

        [Description("发布文章")]
        public const string Publish = "publish:article";

        [Description("获取文章")]
        public const string Get = "get:article";

        [Description("获取文章分页")]
        public const string Page = "page:article";

        [Description("获取文章排名")]
        public const string Ranking = "ranking:article";

        [Description("获取文章分页汇总")]
        public const string SummaryPage = "anlyanis:summary:page:article";
    }
}
