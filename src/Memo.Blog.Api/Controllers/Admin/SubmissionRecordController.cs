using Memo.Blog.Application.SubmissionRecords.Queries.List;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 提交记录
/// </summary>
/// <param name="mediator"></param>
public class SubmissionRecordController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 提交记录列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<Result> ListAsync([FromQuery] ListSubmissionRecordQuery request)
    {
        return await mediator.Send(request);
    }
}
