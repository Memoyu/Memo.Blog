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
    public ArticleCategoryBson Category { get; set; } = new();

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
    /// 文章横幅图
    /// </summary>
    public string Banner { get; set; } = string.Empty;

    /// <summary>
    /// 文章缩略图封面图
    /// </summary>
    public string Thumbnail { get; set; } = string.Empty;

    /// <summary>
    /// 字数
    /// </summary>
    public int WordNumber { get; set; }

    /// <summary>
    /// 预计阅读时长
    /// </summary>
    public int ReadingTime { get; set; }

    /// <summary>
    /// 文章状态
    /// </summary>
    public ArticleStatus Status { get; set; }

    /// <summary>
    /// 浏览次数
    /// </summary>
    public int Views { get; set; }

    /// <summary>
    /// 点赞次数
    /// </summary>
    public int Likes { get; set; }

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

    /// <summary>
    /// 关联标签
    /// </summary>
    public List<ArticleTagBson> Tags { get; set; } = new();

    /// <summary>
    /// 评论
    /// </summary>
    public List<ArticleCommentBson> Comments { get; set; } = new();

    /// <summary>
    /// 作者
    /// </summary>
    public ArticleAuthorBson Author { get; set; } = new();
}

/// <summary>
/// 文章评论Bson
/// </summary>
public class ArticleCommentBson
{
    /// <summary>
    /// 评论Id
    /// </summary>
    public long CommentId { get; set; }

    /// <summary>
    /// 父评论Id
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 所属Id（文章Id、动态Id等）
    /// </summary>
    public long BelongId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    ///  头像url
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 头像来源类型
    /// </summary>
    public AvatarOriginType AvatarOriginType { get; set; }

    /// <summary>
    /// 头像来源
    /// </summary>
    public string AvatarOrigin { get; set; } = string.Empty;

    /// <summary>
    /// 评论所在IP
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 是否展示
    /// </summary>
    public bool Showable { get; set; }
}

/// <summary>
/// 文章分类Bson
/// </summary>
public class ArticleCategoryBson
{
    /// <summary>
    /// 分类Id
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// 文章标签Bson
/// </summary>
public class ArticleTagBson
{
    /// <summary>
    /// 分类Id
    /// </summary>
    public long TagId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// 文章作者Bson
/// </summary>
public class ArticleAuthorBson
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
}
