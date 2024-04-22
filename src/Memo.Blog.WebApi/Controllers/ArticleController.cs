using Memo.Blog.Application.Articles.Queries.Get;
using Memo.Blog.Application.Articles.Queries.Page;
using Memo.Blog.Application.Articles.Queries.Anlyanis;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 文章管理
/// </summary>
public class ArticleController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取文章
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetArticleClientQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取文章分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> GetPageAsync([FromQuery] PageArticleClientQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取文章分页列表汇总
    /// </summary>
    /// <returns></returns>
    [HttpGet("page/summary")]
    public async Task<Result> GetPageSummaryAsync([FromQuery] SummaryArticleQuery request)
    {
        return await mediator.Send(request);
    }
}
