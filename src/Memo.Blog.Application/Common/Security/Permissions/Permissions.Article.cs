﻿namespace Memo.Blog.Application.Common.Security.Permissions;

public static partial class Permissions
{
    public static class Article
    {
        public const string Create = "create:article";
        public const string Set = "set:article";
        public const string Get = "get:article";
        public const string Delete = "delete:article";
    }
}
