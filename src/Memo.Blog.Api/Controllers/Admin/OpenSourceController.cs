using Memo.Blog.Application.OpenSources.Commands.Create;
using Memo.Blog.Application.OpenSources.Commands.Delete;
using Memo.Blog.Application.OpenSources.Commands.Update;
using Memo.Blog.Application.OpenSources.Queries.Get;
using Memo.Blog.Application.OpenSources.Queries.List;
using Memo.Blog.Application.OpenSources.Queries.Page;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 开源项目管理
/// </summary>
/// <param name="mediator"></param>
public class OpenSourceController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建开源项目
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateProjectCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新开源项目
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateProjectCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除开源项目
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteProjectCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取开源项目
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetProjectQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取开源项目分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListProjectQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取开源源项目分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("page/repos/github")]
    public async Task<Result> GitHubRepoPageAsync([FromQuery] PageGitHubRepoQuery request)
    {
        return await mediator.Send(request);
    }
}
