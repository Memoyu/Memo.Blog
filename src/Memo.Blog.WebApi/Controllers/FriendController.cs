using Memo.Blog.Application.Friends.Commands.Create;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 友链管理
/// </summary>
[Route("api/friend")]
[Authorize]
public class FriendController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建友链
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateFriendCommand request)
    {
        return await _mediator.Send(request);
    }
}
