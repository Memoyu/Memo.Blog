namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("文章模板")]
    public static class ArticleTemplate
    {
        [Description("创建文章模板")]
        public const string Create = "create:article-template";

        [Description("更新文章模板")]
        public const string Update = "update:article-template";

        [Description("删除文章模板")]
        public const string Delete = "delete:article-template";

        [Description("获取文章模板")]
        public const string Get = "get:article-template";

        [Description("获取文章模板列表")]
        public const string List = "list:article-template";
    }
}
