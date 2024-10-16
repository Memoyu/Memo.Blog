﻿using Memo.Blog.Application.Moments.Commands.Update;
using Memo.Blog.Application.Moments.Queries.Page;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 动态
/// </summary>
public class MomentController(ISender mediator) : ApiController
{
    /// <summary>
    /// 获取动态列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync([FromQuery] PageMomentClientQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 点赞动态
    /// </summary>
    /// <returns></returns>
    [HttpPost("like")]
    public async Task<Result> LikeAsync(LikeMomentCommand request)
    {
        return await mediator.Send(request);
    }
}
