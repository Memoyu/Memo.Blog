using Memo.Blog.Application.Moments.Commands.Create;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 动态管理
/// </summary>
[Route("api/moment")]

public class MomentController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建动态
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateMomentCommand request)
    {
        return await _mediator.Send(request);
    }
}
