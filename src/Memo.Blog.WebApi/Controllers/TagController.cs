using Memo.Blog.Application.Tags.Commands.Create;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章标签管理
/// </summary>
[Route("api/tag")]
[Authorize]
public class TagController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建标签
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateTagCommand request)
    {
        return await _mediator.Send(request);
    }
}
