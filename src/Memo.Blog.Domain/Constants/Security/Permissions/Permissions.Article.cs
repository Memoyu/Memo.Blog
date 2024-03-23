namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    public static class Article
    {
        public const string Create = "create:article";
        public const string Update = "update:article";
        public const string Delete = "delete:article";
        public const string Publish = "publish:article";

        public const string Get = "get:article";
        public const string Page = "page:article";
    }
}
