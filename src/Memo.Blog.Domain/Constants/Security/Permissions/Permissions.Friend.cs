namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    public static class Friend
    {
        [Description("创建友链")]
        public const string Create = "create:friend";

        [Description("更新友链")]
        public const string Update = "update:friend";

        [Description("删除友链")]
        public const string Delete = "delete:friend";

        [Description("获取友链")]
        public const string Get = "get:friend";

        [Description("获取友链分页")]
        public const string Page = "page:friend";
    }
}
