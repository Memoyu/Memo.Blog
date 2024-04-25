using Memo.Blog.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Memo.Blog.Domain.Entities.Mongo;

/// <summary>
/// 访问日志
/// </summary>
[MongoCollection("visit-logs")]
public class LoggerVisitCollection
{
    /// <summary>
    /// 访问日志Id
    /// </summary>
    [BsonId]
    public long VisitId { get; set; }

    /// <summary>
    /// 访问者标识Id
    /// </summary>
    public long VisitorId { get; set; }

    /// <summary>
    /// 访问路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 访问行为
    /// </summary>
    public VisitLogBehavior Behavior { get; set; }

    /// <summary>
    /// 被访问信息Id（文章Id、动态Id等）
    /// </summary>
    public long? VisitedId { get; set; }

    /// <summary>
    /// 访问者所在IP
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属国家
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属区域
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属省市
    /// </summary>
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属城市
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属互联网服务商
    /// </summary>
    public string Isp { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统
    /// </summary>
    public string Os { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器
    /// </summary>
    public string Browser { get; set; } = string.Empty;

    /// <summary>
    /// 访问时间
    /// </summary>
    public DateTime VisitDate { get; set; }

}
