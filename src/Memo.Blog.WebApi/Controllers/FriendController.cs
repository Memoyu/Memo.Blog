using Memo.Blog.Application.Friends.Commands.Create;
using Memo.Blog.Application.Friends.Commands.Delete;
using Memo.Blog.Application.Friends.Commands.Update;
using Memo.Blog.Application.Friends.Queries.Get;
using Memo.Blog.Application.Friends.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 友链管理
/// </summary>
public class FriendController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取友链
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetFriendQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取友链列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync([FromQuery] PageFriendQuery request)
    {
        return await mediator.Send(request);
    }
}
