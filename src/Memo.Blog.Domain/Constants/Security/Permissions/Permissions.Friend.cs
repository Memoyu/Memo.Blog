namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    public static class Friend
    {
        public const string Create = "create:friend";
        public const string Set = "set:friend";
        public const string Get = "get:friend";
        public const string Delete = "delete:friend";
    }
}
