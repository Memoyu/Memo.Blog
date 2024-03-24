using Serilog.Sinks.MongoDB;

namespace Memo.Blog.Domain.Entities.Mongo;

/// <summary>
/// 系统日志
/// </summary>
[MongoCollection("logs")]
public class LoggerSystemCollection : LogEntry
{

}
