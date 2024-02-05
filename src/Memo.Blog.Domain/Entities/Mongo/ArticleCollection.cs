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
    /// 所属分类Id
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 正文
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
