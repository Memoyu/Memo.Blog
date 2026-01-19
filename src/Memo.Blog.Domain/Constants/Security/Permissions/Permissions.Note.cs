namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("笔记")]
    public static class Note
    {
        [Description("创建笔记")]
        public const string Create = "create:note";

        [Description("创建笔记分组")]
        public const string CreateGroup = "create:note:group";

        [Description("更新笔记")]
        public const string Update = "update:note";

        [Description("更新笔记分组")]
        public const string UpdateGroup = "update:note:group";

        [Description("更新笔记/分组标题")]
        public const string UpdateTitle = "update:note/group:title";

        [Description("删除笔记")]
        public const string Delete = "delete:note";

        [Description("删除笔记分组")]
        public const string DeleteGroup = "delete:note:group";

        [Description("获取笔记目录列表")]
        public const string Catalog = "list:catalog";

        [Description("获取笔记")]
        public const string Get = "get:note";
    }
}
