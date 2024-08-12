using Memo.Blog.Application.Configs.Commands.Update;
using Memo.Blog.Application.Configs.Queries.Get;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 系统配置
/// </summary>
/// <param name="mediator"></param>
public class ConfigController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 更新系统配置
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateConfigCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取系统配置
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetConfigQuery request)
    {
        return await mediator.Send(request);
    }
}
