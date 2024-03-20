using Memo.Blog.Application.Categories.Common;
using Memo.Blog.Application.Tags.Common;
using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Articles.Common;

public class ArticlePageResult
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 所属分类
    /// </summary>
    public required CategoryResult Category { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 关联标签
    /// </summary>
    public List<TagResult> Tags { get; set; } = [];

    /// <summary>
    /// 文章状态
    /// </summary>
    public ArticleStatus Status { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 是否开启评论
    /// </summary>
    public bool Commentable { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    public bool Publicable { get; set; }
}
