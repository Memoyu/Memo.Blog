using Memo.Blog.Application.Moments.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 动态
/// </summary>
public class MomentController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取动态列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync([FromQuery] PageMomentClientQuery request)
    {
        return await mediator.Send(request);
    }
}
