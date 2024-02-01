namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/user")]
[Authorize]
public class UserController : ApiController
{
    /// <summary>
    /// 获取用户
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync()
    {
        return Result.Success();
    }
}
