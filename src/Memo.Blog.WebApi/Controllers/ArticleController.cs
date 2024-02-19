using Memo.Blog.Application.Articles.Commands.Create;
using Memo.Blog.Application.Articles.Queries.Get;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章管理
/// </summary>
[Route("api/article")]

public class ArticleController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建文章
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateArticleCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取文章
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetArticleQuery request)
    {
        return await _mediator.Send(request);
    }
}
