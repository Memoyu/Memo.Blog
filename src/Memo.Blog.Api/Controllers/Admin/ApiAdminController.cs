namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// Admin API基类
/// </summary>
[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public abstract class ApiAdminController : ControllerBase
{
}
