using Memo.Blog.Application.Tokens.Commands.Refresh;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 用户授权
/// </summary>
/// <param name="mediator"></param>
[AllowAnonymous]
public class TokenController(ISender mediator) : ApiAdminController
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
    
    /// <summary>
    /// 刷新用户Token
    /// </summary>
    /// <returns></returns>
    [HttpGet("refresh")]
    public async Task<Result> RefreshAsync()
    {
        Request.Headers.TryGetValue("refresh-token", out var refreshToken);
        RefreshTokenCommand request = new(refreshToken.FirstOrDefault() ?? string.Empty);
        return await mediator.Send(request);
    }
}
