namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("开源项目")]
    public static class OpenSource
    {
        [Description("创建开源项目")]
        public const string Create = "create:open-source";

        [Description("更新开源项目")]
        public const string Update = "update:open-source";

        [Description("删除开源项目")]
        public const string Delete = "delete:open-source";

        [Description("获取开源项目")]
        public const string Get = "get:open-source";

        [Description("获取GitHub开源项目分页列表")]
        public const string GitHubRepoPage = "page:github:open-source";

        [Description("获取开源项目列表")]
        public const string List = "list:open-source";
    }
}
