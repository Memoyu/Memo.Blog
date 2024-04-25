using System.Text.Json;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Loggers.Common;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Enums;
using MongoDB.Bson.Serialization;

namespace Memo.Blog.Application.Common.Mappings;

public class LoggerRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.ForType<LoggerSystemCollection, LoggerSystemPageResult>()
            .Map(d => d.Id, s => s.Id!.ToString())
            .Map(d => d.Message, s => s.RenderedMessage)
            .Map(d => d.Source, s => GetStringLogProperties(s, "SourceContext"))
            .Map(d => d.Request, s => GetJsonLogRequest(s))
            .Map(d => d.RequestId, s => GetStringLogProperties(s, "RequestId"))
            .Map(d => d.RequestPath, s => GetStringLogProperties(s, "RequestPath"))
            .Map(d => d.ExMessage, s => GetStringLogException(s, "Message"))
            .Map(d => d.Time, s => s.UtcTimeStamp.ToLocalTime());

        config.ForType<LoggerSystemCollection, LoggerSystemResult>()
            .Map(d => d.Id, s => s.Id!.ToString())
            .Map(d => d.Message, s => s.RenderedMessage)
            .Map(d => d.Source, s => GetStringLogProperties(s, "SourceContext"))
            .Map(d => d.ActionId, s => GetStringLogProperties(s, "ActionId"))
            .Map(d => d.ActionName, s => GetStringLogProperties(s, "ActionName"))
            .Map(d => d.Request, s => GetJsonLogRequest(s))
            .Map(d => d.RequestId, s => GetStringLogProperties(s, "RequestId"))
            .Map(d => d.RequestPath, s => GetStringLogProperties(s, "RequestPath"))
            .Map(d => d.ExSource, s => GetStringLogException(s, "Source"))
            .Map(d => d.ExMessage, s => GetStringLogException(s, "Message"))
            .Map(d => d.ExStackTrace, s => GetStringLogException(s, "StackTrace"))
            .Map(d => d.Time, s => s.UtcTimeStamp.ToLocalTime());

        config.ForType<LoggerVisitCollection, LoggerVisitPageResult>()
            .Map(d => d.Visited, s => GetLoggerVisited(s));

    }

    private static string GetJsonLogRequest(LoggerSystemCollection s)
    {
        if (s.Properties == null || !s.Properties.Names.Any(n => n.Equals("Request")) || s.Properties["Request"] == null) return string.Empty;
        var json = BsonSerializer.Deserialize<object>(s.Properties["Request"].AsBsonDocument);
        return JsonSerializer.Serialize(json);
    }

    private static string GetStringLogProperties(LoggerSystemCollection s, string field)
    {
        return s.Properties == null || !s.Properties.Names.Any(n => n.Equals(field)) || s.Properties[field] == null ? string.Empty : s.Properties[field].AsString;
    }

    private static string GetStringLogException(LoggerSystemCollection s, string field)
    {
        return s.Exception == null || !s.Exception.Names.Any(n => n.Equals(field)) || s.Exception[field] == null ? string.Empty : s.Exception[field].AsString;
    }

    private LoggerVisitedResult GetLoggerVisited(LoggerVisitCollection s)
    {
        var belong = new LoggerVisitedResult();
        switch (s.Behavior)
        {
            case VisitLogBehavior.Home:
                belong = new LoggerVisitedResult { Id = s.VisitedId, Title = VisitLogBehavior.Home.GetDescription(), Link = "" };
                break;

            case VisitLogBehavior.Article:
                belong = new LoggerVisitedResult { Id = s.VisitedId, Title = VisitLogBehavior.Article.GetDescription(), Link = "" };
                break;

            case VisitLogBehavior.ArticleDetail:
                var article = MapContext.Current.GetService<IBaseDefaultRepository<Article>>().Select
                    .Where(c => c.ArticleId == s.VisitedId)
                    .First();
                belong = article != null
                    ? new LoggerVisitedResult { Id = s.VisitedId, Title = article.Title, Link = "" }
                    : new LoggerVisitedResult { Id = s.VisitedId, Title = VisitLogBehavior.ArticleDetail.GetDescription(), Link = "" };

                break;

            case VisitLogBehavior.Labs:
                belong = new LoggerVisitedResult { Id = s.VisitedId, Title = VisitLogBehavior.Labs.GetDescription(), Link = "" };
                break;

            case VisitLogBehavior.Moment:
                belong = new LoggerVisitedResult { Id = s.VisitedId, Title = VisitLogBehavior.Moment.GetDescription(), Link = "" };
                break;

            case VisitLogBehavior.About:
                belong = new LoggerVisitedResult { Id = s.VisitedId, Title = VisitLogBehavior.About.GetDescription(), Link = "" };
                break;
        }

        return belong;
    }
}
