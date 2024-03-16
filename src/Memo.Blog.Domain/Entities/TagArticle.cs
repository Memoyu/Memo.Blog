﻿using static Memo.Blog.Domain.Constants.Security.Permissions.Permissions;

namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章标签
/// </summary>
[Table(Name = "tag_article")]
public class TagArticle : BaseAuditEntity
{
    /// <summary>
    /// 标签Id
    /// </summary>
    [Description("标签Id")]
    public long TagId { get; set; }

    /// <summary>
    /// 文章Id
    /// </summary>
    [Description("文章Id")]
    public long ArticleId { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [Navigate(nameof(Tag.TagId), TempPrimary = nameof(TagId))]
    public virtual Tag Tag { get; set; }

    /// <summary>
    /// 文章
    /// </summary>
    [Navigate(nameof(Article.ArticleId), TempPrimary = nameof(ArticleId))]
    public virtual Article Article { get; set; }
}
