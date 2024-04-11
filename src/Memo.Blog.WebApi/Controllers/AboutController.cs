using Memo.Blog.Application.Abouts.Queries.Get;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 关于管理
/// </summary>
public class AboutController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取关于信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetAboutQuery request)
    {
        return await mediator.Send(request);
    }
}
