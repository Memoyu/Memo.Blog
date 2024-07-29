using Memo.Blog.Application.Friends.Commands.View;
using Memo.Blog.Application.Friends.Queries.List;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 友链
/// </summary>
public class FriendController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取友链列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> PageAsync([FromQuery] ListFriendQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 访问友链
    /// </summary>
    /// <returns></returns>
    [HttpPost("view")]
    public async Task<Result> ViewAsync(ViewFriendCommand request)
    {
        return await mediator.Send(request);
    }
}
