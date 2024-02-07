using Memo.Blog.Application.Permissions.Commands.Create;

namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 权限管理
/// </summary>
[Route("api/permission")]

public class PermissionController(ISender _mediator) : ApiController
{
    /// <summary>
    /// 创建权限
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreatePermissionCommand request)
    {
        return await _mediator.Send(request);
    }
}
