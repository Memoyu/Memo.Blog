using Memo.Blog.Application.Comments.Commands.Create;

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
}
