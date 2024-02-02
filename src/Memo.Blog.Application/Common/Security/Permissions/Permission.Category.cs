namespace Memo.Blog.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Category
    {
        public const string Create = "create:category";
        public const string Set = "set:category";
        public const string Get = "get:category";
        public const string Delete = "delete:category";
    }
}
