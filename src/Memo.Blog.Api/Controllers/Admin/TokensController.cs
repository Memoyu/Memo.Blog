﻿namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 用户授权
/// </summary>
/// <param name="mediator"></param>
[AllowAnonymous]
public class TokensController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 生成用户Token
    /// </summary>
    /// <param name="request">用户账户、密码</param>
    /// <returns></returns>
    [HttpPost("generate")]
    public async Task<Result> GenerateAsync(GenerateTokenCommand request)
    {
        return await mediator.Send(request);
    }
}
