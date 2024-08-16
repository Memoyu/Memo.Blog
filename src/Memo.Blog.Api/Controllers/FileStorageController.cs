using Memo.Blog.Application.FileStorages.Queries.Generate;

namespace Memo.Blog.Api.Controllers;

/// <summary>
/// 文件存储
/// </summary>
public class FileStorageController(ISender mediator) : ApiController
{
    [HttpGet("qiniu/generate")]
    public async Task<Result> QiniuGenerateAsync([FromQuery] GenerateClientQiniuUploadTokenQuery request)
    {
        return await mediator.Send(request);
    }
}
