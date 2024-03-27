using Memo.Blog.Application.Users.Commands.Create;
using Memo.Blog.Application.Users.Queries.Get;
using Memo.Blog.Application.Users.Queries.Page;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/user")]
public class UserController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateUserCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync(GetUserQuery request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 获取用户分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<Result> PageAsync(PageUserQuery request)
    {
        return await _mediator.Send(request);
    }
}
