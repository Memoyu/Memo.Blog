namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("笔记")]
    public static class Note
    {
        [Description("创建笔记")]
        public const string Create = "create:note";

        [Description("创建笔记目录")]
        public const string CreateCatalog = "create:note:catalog";

        [Description("更新笔记")]
        public const string Update = "update:note";

        [Description("更新笔记目录")]
        public const string UpdateCatalog = "update:note:catalog";

        [Description("删除笔记")]
        public const string Delete = "delete:note";

        [Description("删除笔记目录")]
        public const string DeleteCatalog = "delete:note:catalog";

        [Description("获取笔记")]
        public const string Get = "get:note";
    }
}
