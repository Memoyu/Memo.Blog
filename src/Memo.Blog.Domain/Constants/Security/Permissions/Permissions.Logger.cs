namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("访问日志")]
    public static class LoggerAccess
    {
        [Description("创建访问日志")]
        public const string Create = "create:logger:access";

        [Description("获取访问日志")]
        public const string Get = "get:logger:access";

        [Description("获取访问日志分页")]
        public const string Page = "page:logger:access";
    }

    [Description("系统日志")]
    public static class LoggerSystem
    {
        [Description("获取系统日志")]
        public const string Get = "get:logger:system";

        [Description("获取系统日志分页")]
        public const string Page = "page:logger:system";
    }
}
