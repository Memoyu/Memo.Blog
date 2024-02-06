namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    public static class Comment
    {
        public const string Create = "create:comment";
        public const string Set = "set:comment";
        public const string Get = "get:comment";
        public const string Delete = "delete:comment";
    }
}
