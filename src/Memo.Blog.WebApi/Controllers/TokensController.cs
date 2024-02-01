namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 用户登录
/// </summary>
/// <param name="_mediator"></param>
[Route("api/tokens")]
[AllowAnonymous]
public class TokensController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="request">用户账户、密码</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<Result> LoginAsync(GenerateTokenQuery request)
    {
        return await _mediator.Send(request);
    }
}
