using Memo.Blog.Application.OpenSources.Queries.Get;
using Memo.Blog.Application.OpenSources.Queries.List;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 开源项目
/// </summary>
/// <param name="mediator"></param>
public class OpenSourceController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取开源项目
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetProjectClientQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取开源项目分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListProjectClientQuery request)
    {
        return await mediator.Send(request);
   }
}
