using Memo.Blog.Application.Articles.Commands.Create;
using Memo.Blog.Application.Articles.Commands.Delete;
using Memo.Blog.Application.Articles.Commands.Update;
using Memo.Blog.Application.Articles.Queries.Get;
using Memo.Blog.Application.Articles.Queries.Page;
using Memo.Blog.Application.Articles.Queries.Anlyanis;
using Memo.Blog.Application.Articles.Queries.Ranking;

namespace Memo.Blog.WebApi.Controllers.Admin;

/// <summary>
/// 文章管理
/// </summary>
public class ArticleController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建文章
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateArticleCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新文章
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateArticleCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteArticleCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取文章
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetArticleQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取文章排名列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("ranking")]
    public async Task<Result> GetRankingAsync([FromQuery] RankingArticleQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取文章分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> GetPageAsync([FromQuery] PageArticleQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取文章汇总
    /// </summary>
    /// <returns></returns>
    [HttpGet("summary")]
    public async Task<Result> GetPageSummaryAsync([FromQuery] SummaryArticleQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 发布文章
    /// </summary>
    /// <returns></returns>
    [HttpPut("publish")]
    public async Task<Result> PublishAsync(PublishArticleCommand request)
    {
        return await mediator.Send(request);
    }
}
