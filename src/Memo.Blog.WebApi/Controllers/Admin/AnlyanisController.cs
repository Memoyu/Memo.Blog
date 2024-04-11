using Memo.Blog.Application.Anlyanis.Queries.Dashboard;

namespace Memo.Blog.WebApi.Controllers.Admin;

/// <summary>
/// 数据统计分析
/// </summary>
/// <param name="mediator"></param>
public class AnlyanisController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 获取概览分析数据
    /// </summary>
    /// <returns></returns>
    [HttpGet("dashboard")]
    public async Task<Result> GetDashboardAsync([FromQuery] DashboardAnlyanisQuery request)
    {
        return await mediator.Send(request);
    }
}
