namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 用户授权
/// </summary>
/// <param name="_mediator"></param>
[Route("api/tokens")]
[AllowAnonymous]
public class TokensController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 生成用户Token
    /// </summary>
    /// <param name="request">用户账户、密码</param>
    /// <returns></returns>
    [HttpPost("generate")]
    public async Task<Result> GenerateAsync(GenerateTokenQuery request)
    {
        return await _mediator.Send(request);
    }
}
