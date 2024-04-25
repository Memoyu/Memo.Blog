using Memo.Blog.Application.Visitors.Commands.Generate;
using Memo.Blog.Application.Visitors.Commands.Update;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 访客
/// </summary>
public class VisitorController(ISender mediator) : ApiController
{
    /// <summary>
    /// 生成访客
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateVisitorCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新访客
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateVisitorCommand request)
    {
        return await mediator.Send(request);
    }
}
