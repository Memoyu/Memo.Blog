using Memo.Blog.Application.Anlyanis.Queries.Dashboard;

namespace Memo.Blog.Api.Controllers.Admin;

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

    /// <summary>
    /// 获取访客地图分析数据
    /// </summary>
    /// <returns></returns>
    [HttpGet("uv/map")]
    public async Task<Result> GetUvMapAsync([FromQuery] UvMapAnlyanisQuery request)
    {
        return await mediator.Send(request);
    }
}
