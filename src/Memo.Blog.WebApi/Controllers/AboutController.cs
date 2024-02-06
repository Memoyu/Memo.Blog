namespace Memo.Blog.WebApi.Controllers;

/// <summary>
/// 关于管理
/// </summary>
[Route("api/about")]
[Authorize]
public class AboutController(ISender _mediator) : ApiController
{
}
