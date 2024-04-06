using Memo.Blog.Application.Common.Models.FileStorages;
using Memo.Blog.Application.FileStorages.Common;
using Microsoft.Extensions.Options;
using Qiniu.Storage;
using Qiniu.Util;

namespace Memo.Blog.Application.FileStorages.Queries.Generate;

public class GenerateQiniuUploadTokenQueryHandler(IOptionsMonitor<QiniuOptions> qiniuOptions) : IRequestHandler<GenerateQiniuUploadTokenQuery, Result>
{
    public async Task<Result> Handle(GenerateQiniuUploadTokenQuery request, CancellationToken cancellationToken)
    {
        var options = qiniuOptions.CurrentValue;
        var sign = new Signature(new Mac(options.AK, options.SK));
        var policy = new PutPolicy
        {
            Scope = string.IsNullOrWhiteSpace(request.Path) ? options.Bucket : $"{options.Bucket}:{request.Path}"
        };

        var token = sign.SignWithData(policy.ToJsonString());

        await Task.CompletedTask;
        return Result.Success(new QiniuUploadTokenResult { Token = token, Host = options.Host });
    }
}
