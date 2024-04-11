using Memo.Blog.Application.Comments.Queries.Get;
using Memo.Blog.Application.Comments.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 评论管理
/// </summary>
public class CommentController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取评论
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetCommentQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取评论分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> GetPageAsync([FromQuery] PageCommentQuery request)
    {
        return await mediator.Send(request);
    }
}
