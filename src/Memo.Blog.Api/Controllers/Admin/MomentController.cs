﻿using Memo.Blog.Application.Moments.Commands.Create;
using Memo.Blog.Application.Moments.Commands.Delete;
using Memo.Blog.Application.Moments.Commands.Update;
using Memo.Blog.Application.Moments.Queries.Get;
using Memo.Blog.Application.Moments.Queries.Page;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 动态管理
/// </summary>
public class MomentController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建动态
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateMomentCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新动态
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateMomentCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除动态
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteMomentCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取动态
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetMomentQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取动态列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync([FromQuery] PageMomentQuery request)
    {
        return await mediator.Send(request);
    }
}
