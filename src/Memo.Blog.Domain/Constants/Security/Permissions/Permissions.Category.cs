﻿namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("文章分类")]
    public static class Category
    {
        [Description("创建文章分类")]
        public const string Create = "create:category";

        [Description("更新文章分类")]
        public const string Update = "update:category";

        [Description("删除文章分类")]
        public const string Delete = "delete:category";

        [Description("获取文章分类")]
        public const string Get = "get:category";

        [Description("获取文章分类列表")]
        public const string List = "list:category";

        [Description("获取分类关联文章汇总")]
        public const string RelationSummary = "anlyanis:relation:summary:category";
    }
}
