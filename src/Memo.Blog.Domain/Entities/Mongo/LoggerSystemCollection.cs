using Memo.Blog.Domain.Constants;
using Serilog.Sinks.MongoDB;

namespace Memo.Blog.Domain.Entities.Mongo;

/// <summary>
/// 系统日志
/// </summary>
[MongoCollection(AppConst.SystemLogCollectionName)]
public class LoggerSystemCollection : LogEntry
{

}
