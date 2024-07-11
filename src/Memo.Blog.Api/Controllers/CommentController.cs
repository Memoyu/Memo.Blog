using Memo.Blog.Application.Comments.Commands.Create;
using Memo.Blog.Application.Comments.Queries.Page;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 评论
/// </summary>
public class CommentController(ISender mediator) : ApiController
{
    /// <summary>
    /// 创建评论
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateCommentClientCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取评论分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> GetPageAsync([FromQuery] PageCommentClientQuery request)
    {
        return await mediator.Send(request);
    }
}
