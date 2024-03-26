namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("用户")]
    public static class User
    {
        [Description("创建用户")]
        public const string Create = "create:user";

        [Description("更新用户")]
        public const string Update = "update:user";

        [Description("删除用户")]
        public const string Delete = "delete:user";

        [Description("获取用户")]
        public const string Get = "get:user";

        [Description("获取用户分页")]
        public const string Page = "page:user";
    }
}
