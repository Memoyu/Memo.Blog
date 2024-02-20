using Memo.Blog.Application.Categories.Commands.Create;
using Memo.Blog.Application.Categories.Commands.Delete;
using Memo.Blog.Application.Categories.Queries.Get;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章分类管理
/// </summary>
[Route("api/category")]
public class CategoryController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建分类
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateCategoryCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync(DeleteCategoryCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取分类
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetCategoryQuery request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 分类列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListCategoryQuery request)
    {
        return await _mediator.Send(request);
    }
}
