namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("数据统计")]
    public static class Anlyanis
    {
        [Description("获取概览页统计数据")]
        public const string Dashboard = "dashboard:anlyanis";

        [Description("获取概览页统计数据")]
        public const string UniqueVisitorMap = "uv:map:anlyanis";
    }
}
