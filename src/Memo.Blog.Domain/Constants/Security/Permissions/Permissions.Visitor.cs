namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("访客")]
    public static class Visitor
    {
        [Description("更新访客")]
        public const string Update = "update:visitor";

        [Description("删除访客")]
        public const string Delete = "delete:friend";

        [Description("获取访客")]
        public const string Get = "get:visitor";

        [Description("获取访客分页")]
        public const string Page = "page:visitor";
    }
}
