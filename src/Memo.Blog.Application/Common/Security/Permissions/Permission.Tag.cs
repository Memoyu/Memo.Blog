namespace Memo.Blog.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Tag
    {
        public const string Create = "create:tag";
        public const string Set = "set:tag";
        public const string Get = "get:tag";
        public const string Delete = "delete:tag";
    }
}
