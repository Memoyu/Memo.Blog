using Memo.Blog.Application.Moments.Queries.Get;
using Memo.Blog.Application.Moments.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 动态管理
/// </summary>
public class MomentController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取动态
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetMomentQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取动态列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync([FromQuery] PageMomentQuery request)
    {
        return await mediator.Send(request);
    }
}
