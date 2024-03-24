using Memo.Blog.Application.Abouts.Commands.Update;
using Memo.Blog.Application.Abouts.Queries.Get;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 关于管理
/// </summary>
[Route("api/about")]
public class AboutController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 更新关于信息
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateAboutCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取关于信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetAboutQuery request)
    {
        return await _mediator.Send(request);
    }
}
