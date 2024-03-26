namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("动态")]
    public static class Moment
    {
        [Description("创建动态")]
        public const string Create = "create:moment";

        [Description("更新动态")]
        public const string Update = "update:moment";

        [Description("删除动态")]
        public const string Delete = "delete:moment";

        [Description("获取动态")]
        public const string Get = "get:moment";

        [Description("获取动态分页")]
        public const string Page = "page:moment";
    }
}
