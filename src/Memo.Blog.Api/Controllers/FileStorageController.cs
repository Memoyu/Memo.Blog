using Memo.Blog.Application.FileStorages.Queries.Generate;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 文件存储
/// </summary>
public class FileStorageController(ISender mediator) : ApiController
{
    /// <summary>
    /// 七牛云生成上传凭证
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("qiniu/generate")]
    public async Task<Result> QiniuGenerateAsync([FromQuery] GenerateClientQiniuUploadTokenQuery request)
    {
        return await mediator.Send(request);
    }
}
