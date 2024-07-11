using Memo.Blog.Application.FileStorages.Queries.Generate;

namespace Memo.Blog.Api.Controllers.Admin;

/// <summary>
/// 文件存储管理
/// </summary>
public class FileStorageController(ISender mediator) : ApiAdminController
{
    /// <summary>
    /// 生成七牛云Token
    /// </summary>
    /// <param name="request">存储路径</param>
    /// <returns></returns>
    [HttpGet("qiniu/generate")]
    public async Task<Result> QiniuGenerateAsync([FromQuery] GenerateQiniuUploadTokenQuery request)
    {
        return await mediator.Send(request);
    }
}
