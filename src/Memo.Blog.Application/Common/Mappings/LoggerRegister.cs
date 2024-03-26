﻿using System.Text.Json;
using Memo.Blog.Application.Loggers.Common;
using Memo.Blog.Domain.Entities.Mongo;
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
            .Map(d => d.ExceptionMessage, s => GetStringLogException(s, "Message"))
            .Map(d => d.ExceptionStackTrace, s => GetStringLogException(s, "StackTrace"))
            .Map(d => d.Time, s => s.UtcTimeStamp.ToLocalTime());

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
}
