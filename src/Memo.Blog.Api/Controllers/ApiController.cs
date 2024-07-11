namespace Memo.Blog.Api.Controllers;

/// <summary>
/// API基类
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public abstract class ApiController : ControllerBase
{
}
