namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("文章标签")]
    public static class Tag
    {
        [Description("创建文章标签")]
        public const string Create = "create:tag";

        [Description("更新文章标签")]
        public const string Update = "update:tag";

        [Description("删除文章标签")]
        public const string Delete = "delete:tag";

        [Description("获取文章标签")]
        public const string Get = "get:tag";

        [Description("获取文章标签列表")]
        public const string List = "list:tag";
    }
}
