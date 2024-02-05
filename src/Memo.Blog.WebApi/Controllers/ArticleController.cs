using Memo.Blog.Application.Articles.Commands.CreateArticle;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章管理
/// </summary>
[Route("api/article")]
[Authorize]
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
}
