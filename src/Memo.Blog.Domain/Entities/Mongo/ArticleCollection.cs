using Memo.Blog.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Memo.Blog.Domain.Entities.Mongo;

/// <summary>
/// 文章
/// </summary>
[MongoCollection("article")]
public class ArticleCollection
{
    /// <summary>
    /// 文章Id
    /// </summary>
    [BsonId]
    public long ArticleId { get; set; }

    /// <summary>
    /// 所属分类
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 文章正文
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 关联标签标签名，空格分割
    /// </summary>
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// 评论原文，追加，空格分割
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// 文章状态
    /// </summary>
    public ArticleStatus Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }
}
