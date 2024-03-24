using Memo.Blog.Application.Logger.Queries.Access.Get;
using Memo.Blog.Application.Logger.Queries.Access.Page;
using Memo.Blog.Application.Logger.Queries.System.Get;
using Memo.Blog.Application.Logger.Queries.System.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 友链管理
/// </summary>
[Route("api/looger")]
public class LoggerController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 获取系统日志分页
    /// </summary>
    /// <returns></returns>
    [HttpGet("system/page")]
    public async Task<Result> PageSystemAsync([FromQuery] PageLoggerSystemQuery request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取系统日志详情
    /// </summary>
    /// <returns></returns>
    [HttpGet("system/get")]
    public async Task<Result> GetSystemAsync([FromQuery] GetLoggerSystemQuery request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取访问日志分页
    /// </summary>
    /// <returns></returns>
    [HttpGet("access/page")]
    public async Task<Result> PageAccessAsync([FromQuery] PageLoggerAccessQuery request)
    {
        return await _mediator.Send(request);
    }


    /// <summary>
    /// 获取访问日志详情
    /// </summary>
    /// <returns></returns>
    [HttpGet("access/get")]
    public async Task<Result> GetAccessAsync([FromQuery] GetLoggerAccessQuery request)
    {
        return await _mediator.Send(request);
    }
}
