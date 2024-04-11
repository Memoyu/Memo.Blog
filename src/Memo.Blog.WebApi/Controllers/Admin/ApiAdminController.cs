namespace Memo.Blog.WebApi.Controllers.Admin;

/// <summary>
/// Admin API基类
/// </summary>
[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public abstract class ApiAdminController : ControllerBase
{
}
