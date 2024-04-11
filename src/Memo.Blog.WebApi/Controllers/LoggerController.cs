using Memo.Blog.Application.Loggers.Commands.Visit.Create;
using Memo.Blog.Application.Loggers.Commands.Visit.Generate;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 日志管理
/// </summary>
public class LoggerController(ISender mediator) : ApiController
{
    /// <summary>
    /// 生成访客Id
    /// </summary>
    /// <returns></returns>
    [HttpPost("visit/visitor-id/generate")]
    public async Task<Result> GenerateVisitorIdAsync(GenerateVisitorIdCommand request)
    {
        return await mediator.Send(request);
    }

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
