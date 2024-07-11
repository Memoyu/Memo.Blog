using Memo.Blog.Application.Tags.Queries.List;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 文章标签
/// </summary>
public class TagController(ISender mediator) : ApiController
{
    /// <summary>
    /// 标签列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListTagClientQuery request)
    {
        return await mediator.Send(request);
    }
}
