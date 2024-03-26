using Memo.Blog.Application.Roles.Commands.Create;
using Memo.Blog.Application.Roles.Queries.List;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 角色管理
/// </summary>
[Route("api/role")]
public class RoleController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateRoleCommand request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// 角色列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListRoleQuery request)
    {
        return await _mediator.Send(request);
    }

}
