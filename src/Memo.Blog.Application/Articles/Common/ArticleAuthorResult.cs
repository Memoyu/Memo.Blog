﻿namespace Memo.Blog.Application.Articles.Common;

public record ArticleAuthorResult
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    ///  用户头像url
    /// </summary>
    public string Avatar { get; set; } = string.Empty;
}
