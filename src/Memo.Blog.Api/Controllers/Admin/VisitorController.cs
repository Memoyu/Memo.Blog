using Memo.Blog.Application.Visitors.Commands.Delete;
using Memo.Blog.Application.Visitors.Commands.Update;
using Memo.Blog.Application.Visitors.Queries.Get;
using Memo.Blog.Application.Visitors.Queries.Page;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 访客管理
/// </summary>
public class VisitorController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 更新访客
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateVisitorCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除访客
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteVisitorCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取访客
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetVisitorQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取访客列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync([FromQuery] PageVisitorQuery request)
    {
        return await mediator.Send(request);
    }
}
