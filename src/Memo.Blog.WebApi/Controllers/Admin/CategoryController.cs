using Memo.Blog.Application.Categories.Commands.Create;
using Memo.Blog.Application.Categories.Commands.Delete;
using Memo.Blog.Application.Categories.Commands.Update;
using Memo.Blog.Application.Categories.Queries.Get;
using Memo.Blog.Application.Categories.Queries.List;

namespace Memo.Blog.WebApi.Controllers.Admin;

/// <summary>
/// 文章分类管理
/// </summary>
public class CategoryController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建分类
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateCategoryCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新分类
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateCategoryCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteCategoryCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取分类
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetCategoryQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 分类列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListCategoryQuery request)
    {
        return await mediator.Send(request);
    }
}
