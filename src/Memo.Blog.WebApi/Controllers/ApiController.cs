namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// API基类
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public abstract class ApiController : ControllerBase
{
}
