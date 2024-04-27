using System.Text.Json;
using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Loggers.Commands.Visit.Create;
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
            .Map(d => d.BehaviorName, s => s.Behavior.GetDescription())
            .Map(d => d.Visited, s => GetLoggerVisited(s));

        config.ForType<CreateLoggerVisitCommand, LoggerVisitCollection>()
            .Map(d => d.VisitDate, s => DateTime.Now)
            .Map(d => d.Behavior, s => GetVisitBehavior(s));
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
        var visitedId = s.VisitedId.HasValue ? s.VisitedId.Value : 0;
        switch (s.Behavior)
        {
            case VisitLogBehavior.Home:
                belong = new LoggerVisitedResult { Id = visitedId, Title = VisitLogBehavior.Home.GetDescription() };
                break;

            case VisitLogBehavior.Article:
                belong = new LoggerVisitedResult { Id = visitedId, Title = VisitLogBehavior.Article.GetDescription() };
                break;

            case VisitLogBehavior.ArticleDetail:
                var article = MapContext.Current.GetService<IBaseDefaultRepository<Article>>().Select
                    .Where(c => c.ArticleId == s.VisitedId)
                    .First();
                belong = article != null
                    ? new LoggerVisitedResult { Id = visitedId, Title = article.Title }
                    : new LoggerVisitedResult { Id = visitedId, Title = VisitLogBehavior.ArticleDetail.GetDescription() };

                break;

            case VisitLogBehavior.Labs:
                belong = new LoggerVisitedResult { Id = visitedId, Title = VisitLogBehavior.Labs.GetDescription() };
                break;

            case VisitLogBehavior.Moment:
                belong = new LoggerVisitedResult { Id = visitedId, Title = VisitLogBehavior.Moment.GetDescription() };
                break;

            case VisitLogBehavior.About:
                belong = new LoggerVisitedResult { Id = visitedId, Title = VisitLogBehavior.About.GetDescription() };
                break;
        }

        belong.Link = s.Path;

        return belong;
    }

    private VisitLogBehavior GetVisitBehavior(CreateLoggerVisitCommand req)
    {
        var behavior = VisitLogBehavior.Unknown;
        var ok = Uri.TryCreate(req.Path, UriKind.Absolute, out var uri);
        if (!ok || uri == null) return behavior;

        var abs = uri.AbsolutePath.ToLower();

        // 匹配页面访问行为
        if (abs == "/") return VisitLogBehavior.Home;
        // 得放前面，这样才能匹配到
        if (abs.StartsWith("/article/detail")) return VisitLogBehavior.ArticleDetail;
        if (abs.StartsWith("/article")) return VisitLogBehavior.Article;    
        if (abs.StartsWith("/moment")) return VisitLogBehavior.Moment;
        if (abs.StartsWith("/labs")) return VisitLogBehavior.Labs;
        if (abs.StartsWith("/about")) return VisitLogBehavior.About;

        return behavior;
    }
}
