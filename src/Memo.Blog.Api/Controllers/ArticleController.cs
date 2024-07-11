using Memo.Blog.Application.Articles.Queries.Get;
using Memo.Blog.Application.Articles.Queries.Page;
using Memo.Blog.Application.Articles.Queries.Anlyanis;
using Memo.Blog.Application.Articles.Commands.Update;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 文章
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
    /// 获取文章汇总
    /// </summary>
    /// <returns></returns>
    [HttpGet("summary")]
    public async Task<Result> GetPageSummaryAsync([FromQuery] SummaryArticleClientQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 点赞文章
    /// </summary>
    /// <returns></returns>
    [HttpPost("like")]
    public async Task<Result>LikeAsync(LikeArticleCommand request)
    {
        return await mediator.Send(request);
    }
}
