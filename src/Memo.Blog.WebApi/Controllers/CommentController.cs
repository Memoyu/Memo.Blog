﻿using Memo.Blog.Application.Comments.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 评论
/// </summary>
public class CommentController(ISender mediator) : ApiController
{
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
