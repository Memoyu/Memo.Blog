using Memo.Blog.Application.Tags.Commands.Create;
using Memo.Blog.Application.Tags.Commands.Delete;
using Memo.Blog.Application.Tags.Commands.Update;
using Memo.Blog.Application.Tags.Queries.Anlyanis;
using Memo.Blog.Application.Tags.Queries.Get;
using Memo.Blog.Application.Tags.Queries.List;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 文章标签管理
/// </summary>
public class TagController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建标签
    /// </summary>
    /// <returns></returns>
    [HttpPost(template: "create")]
    public async Task<Result> CreateAsync(CreateTagCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新标签
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateTagCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除标签
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteTagCommand request)
    {
        return await mediator.Send(request);
    }

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

    /// <summary>
    /// 获取标签关联文章汇总
    /// </summary>
    /// <returns></returns>
    [HttpGet("relation/summary")]
    public async Task<Result> GetRelationSummaryAsync([FromQuery] RelationSummaryQuery request)
    {
        return await mediator.Send(request);
    }
}
