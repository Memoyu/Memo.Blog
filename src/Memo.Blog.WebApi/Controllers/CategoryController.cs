using Memo.Blog.Application.Categories.Commands.Create;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章分类管理
/// </summary>
[Route("api/category")]
[Authorize]
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
}
