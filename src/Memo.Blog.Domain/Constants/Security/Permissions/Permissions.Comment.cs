namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("评论")]
    public static class Comment
    {
        [Description("创建评论")]
        public const string Create = "create:comment";

        [Description("更新评论")]
        public const string Update = "update:comment";

        [Description("删除评论")]
        public const string Delete = "delete:comment";

        [Description("获取评论")]
        public const string Get = "get:comment";

        [Description("获取评论分页")]
        public const string Page = "page:comment";

    }
}
