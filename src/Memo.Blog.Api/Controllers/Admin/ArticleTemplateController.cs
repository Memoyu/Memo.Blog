using Memo.Blog.Application.ArticleTemplates.Commands.Create;
using Memo.Blog.Application.ArticleTemplates.Commands.Delete;
using Memo.Blog.Application.ArticleTemplates.Commands.Update;
using Memo.Blog.Application.ArticleTemplates.Queries.Get;
using Memo.Blog.Application.ArticleTemplates.Queries.List;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 文章模板管理
/// </summary>
public class ArticleTemplateController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 创建模板
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<Result> CreateAsync(CreateArticleTemplateCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 更新模板
    /// </summary>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<Result> UpdateAsync(UpdateArticleTemplateCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<Result> DeleteAsync([FromQuery] DeleteArticleTemplateCommand request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取模板
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<Result> GetAsync([FromQuery] GetArticleTemplateQuery request)
    {
        return await mediator.Send(request);
    }

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListArticleTemplateQuery request)
    {
        return await mediator.Send(request);
    }
}
