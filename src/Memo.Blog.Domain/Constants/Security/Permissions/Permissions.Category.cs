namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    public static class Category
    {
        [Description("创建文章分类")]
        public const string Create = "create:category";

        [Description("更新文章分类")]
        public const string Update = "update:category";

        [Description("获取文章分类")]
        public const string Get = "get:category";

        [Description("删除文章分类")]
        public const string Delete = "delete:category";
    }
}
