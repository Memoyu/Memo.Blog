using Serilog.Events;
namespace Memo.Blog.Application.Logger.Queries.System.Page;

[Authorize(Permissions = ApiPermission.LoggerSystem.Page)]
public record PageLoggerSystemQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
    public LogEventLevel? Level { get; set; }

    public string? Message { get; set; }

    public string? Source { get; set; }

    public string? RequestParamterName { get; set; }

    public string? RequestParamterValue { get; set; }

    public string? RequestId { get; set; }

    public string? RequestPath { get; set; }

    public DateTime? TimeBegin { get; set; }

    public DateTime? TimeEnd { get; set; }
}
