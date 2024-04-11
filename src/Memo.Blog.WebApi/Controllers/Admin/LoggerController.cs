using Memo.Blog.Application.Logger.Queries.Visit.Get;
using Memo.Blog.Application.Logger.Queries.Visit.Page;
using Memo.Blog.Application.Logger.Queries.System.Get;
using Memo.Blog.Application.Logger.Queries.System.Page;
using Memo.Blog.Application.Loggers.Commands.Visit.Create;

namespace Memo.Blog.WebApi.Controllers.Admin;

/// <summary>
/// 日志管理
/// </summary>
public class LoggerController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 获取系统日志分页
    /// </summary>
    /// <returns></returns>
    [HttpGet("system/page")]
    public async Task<Result> PageSystemAsync([FromQuery] PageLoggerSystemQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取系统日志详情
    /// </summary>
    /// <returns></returns>
    [HttpGet("system/get")]
    public async Task<Result> GetSystemAsync([FromQuery] GetLoggerSystemQuery request)
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

    /// <summary>
    /// 获取访问日志分页
    /// </summary>
    /// <returns></returns>
    [HttpGet("visit/page")]
    public async Task<Result> PageVisitAsync([FromQuery] PageLoggerVisitQuery request)
    {
        return await mediator.Send(request);
    }


    /// <summary>
    /// 获取访问日志详情
    /// </summary>
    /// <returns></returns>
    [HttpGet("visit/get")]
    public async Task<Result> GetVisitAsync([FromQuery] GetLoggerVisitQuery request)
    {
        return await mediator.Send(request);
    }
}
