using Memo.Blog.Application.Tags.Queries.Get;
using Memo.Blog.Application.Tags.Queries.List;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章标签
/// </summary>
public class TagController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取标签
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetTagQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 标签列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListTagQuery request)
    {
        return await mediator.Send(request);
    }
}
