﻿namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("消息")]
    public static class Message
    {
        [Description("创建消息")]
        public const string Create = "create:message";

        [Description("标为已读消息")]
        public const string Read = "update:read:message";

        [Description("获取消息分页列表")]
        public const string Page = "page:message";
    }
}