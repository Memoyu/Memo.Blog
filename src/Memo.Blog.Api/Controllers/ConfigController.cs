using Memo.Blog.Application.Configs.Queries.Get;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 系统配置
/// </summary>
public class ConfigController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取系统配置
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetConfigClientQuery request)
    {
        return await mediator.Send(request);
    }
}
