using Memo.Blog.Application.Comments.Commands.Create;
using Memo.Blog.Application.Comments.Commands.Delete;
using Memo.Blog.Application.Comments.Commands.Update;
using Memo.Blog.Application.Comments.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 评论管理
/// </summary>
[Route("api/comment")]

public class CommentController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建评论
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateCommentCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 更新评论
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateCommentCommand request)
    {
        return await _mediator.Send(request);
    }


    /// <summary>
    /// 删除评论
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteCommentCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取评论分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> GetPageAsync([FromQuery] PageCommentQuery request)
    {
        return await _mediator.Send(request);
    }
}
