using Memo.Blog.Application.Loggers.Commands.Visit.Create;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 日志
/// </summary>
public class LoggerController(ISender mediator) : ApiController
{
    /// <summary>
    /// 创建访问日志
    /// </summary>
    /// <returns></returns>
    [HttpPost("visit/create")]
    public async Task<Result> CreateVisitAsync(CreateLoggerVisitCommand request)
    {
        return await mediator.Send(request);
    }
}
