namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("关于信息")]
    public static class About
    {
        // public const string Create = "create:about";

        [Description("更新关于信息")]
        public const string Update = "update:about";

        [Description("获取更新关于信息")]
        public const string Get = "get:about";

        // public const string Delete = "delete:about";
    }
}
