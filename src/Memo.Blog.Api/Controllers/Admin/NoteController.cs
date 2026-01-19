using Memo.Blog.Application.Friends.Queries.Get;
using Memo.Blog.Application.Notes.Commands.Create;
using Memo.Blog.Application.Notes.Commands.Delete;
using Memo.Blog.Application.Notes.Commands.Update;
using Memo.Blog.Application.Notes.Queries.List;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 笔记管理
/// </summary>
public class NoteController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建笔记
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateNoteCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新笔记
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateNoteCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除笔记
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteNoteCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取笔记
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetNoteQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 创建笔记分组
    /// </summary>
    /// <returns></returns>
    [HttpPost("create/group")]
    public async Task<Result> CreateGroupAsync(CreateGroupCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新笔记分组
    /// </summary>
    /// <returns></returns>
    [HttpPut("update/group")]
    public async Task<Result> UpdateGroupAsync(UpdateGroupCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新笔记/分组标题
    /// </summary>
    /// <returns></returns>
    [HttpPut("update/title")]
    public async Task<Result> UpdateTitleAsync(UpdateTitleCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除笔记分组
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete/group")]
    public async Task<Result> DeleteGroupAsync([FromQuery] DeleteGroupCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取笔记目录列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list/catalog")]
    public async Task<Result> ListCatalogpAsync([FromQuery] ListCatalogQuery request)
    {
        return await mediator.Send(request);
    }
}
