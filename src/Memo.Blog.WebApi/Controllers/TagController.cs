using Memo.Blog.Application.Tags.Commands.Create;
using Memo.Blog.Application.Tags.Commands.Delete;
using Memo.Blog.Application.Tags.Commands.Update;
using Memo.Blog.Application.Tags.Queries.Get;
using Memo.Blog.Application.Tags.Queries.List;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章标签管理
/// </summary>
[Route("api/tag")]
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

    /// <summary>
    /// 更新标签
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateTagCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 删除标签
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteTagCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取标签
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetTagQuery request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 标签列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListTagQuery request)
    {
        return await _mediator.Send(request);
    }
}
