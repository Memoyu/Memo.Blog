using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Domain.Entities;

/// <summary>
/// 文章表
/// </summary>
[Table(Name = "article")]
[Index("index_on_article_id", nameof(ArticleId), false)]
public class Article : BaseAuditEntity
{
    /// <summary>
    /// 文章Id
    /// </summary>
    [Snowflake]
    [Description("文章Id")]
    [Column(CanUpdate = false)]
    public long ArticleId { get; set; }

    /// <summary>
    /// 所属分类Id
    /// </summary>
    [Description("所属分类Id")]
    public long CategoryId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Description("标题")]
    [Column(StringLength = 200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [Description("描述")]
    [Column(StringLength = 200)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 文章正文
    /// </summary>
    [Description("文章正文")]
    [Column(StringLength = -2)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 文章横幅图
    /// </summary>
    [Description("文章横幅图")]
    public string Banner { get; set; } = string.Empty;

    /// <summary>
    /// 文章缩略图封面图
    /// </summary>
    [Description("文章缩略图封面图")]
    public string Thumbnail { get; set; } = string.Empty;

    /// <summary>
    /// 字数
    /// </summary>
    [Description("字数")]
    public int WordNumber { get; set; }

    /// <summary>
    /// 预计阅读时长
    /// </summary>
    [Description("预计阅读时长")]
    public int ReadingTime { get; set; }

    /// <summary>
    /// 文章状态
    /// </summary>
    [Description("文章状态")]
    public ArticleStatus Status { get; set; }

    /// <summary>
    /// 浏览次数
    /// </summary>
    [Description("浏览次数")]
    public int Views { get; set; }

    /// <summary>
    /// 点赞次数
    /// </summary>
    [Description("点赞次数")]
    public int Likes { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    [Description("是否置顶")]
    public bool IsTop { get; set; }

    /// <summary>
    /// 是否开启评论
    /// </summary>
    [Description("是否开启评论")]
    public bool Commentable { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    [Description("是否公开")]
    public bool Publicable { get; set; }

    /// <summary>
    /// 文章分类
    /// </summary>
    [Navigate(nameof(Category.CategoryId), TempPrimary = nameof(CategoryId))]
    public virtual Category Category { get; set; } = new();

    /// <summary>
    /// 文章关联标签
    /// </summary>
    [Navigate(nameof(ArticleTag.ArticleId), TempPrimary = nameof(ArticleId))]
    public virtual List<ArticleTag> ArticleTags { get; set; } = [];

    /// <summary>
    /// 文章作者
    /// </summary>
    public User Author { get; set; } = new();
}
