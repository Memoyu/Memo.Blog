namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("系统配置")]
    public static class Config
    {
        [Description("更新系统配置")]
        public const string Update = "update:config";

        [Description("更新默认回复访客配置")]
        public const string UpdateVisitor = "update:visitor:config";

        [Description("获取系统配置")]
        public const string Get = "get:config";

        [Description("获取管理员访客配置")]
        public const string GeVisitort = "get:visitor:config";
    }
}

