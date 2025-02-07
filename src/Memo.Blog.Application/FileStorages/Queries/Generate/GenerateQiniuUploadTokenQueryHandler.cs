﻿using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Application.FileStorages.Common;
using Microsoft.Extensions.Options;
using Qiniu.Storage;
using Qiniu.Util;

namespace Memo.Blog.Application.FileStorages.Queries.Generate;

public class GenerateQiniuUploadTokenQueryHandler(IOptionsMonitor<AuthorizationSettings> authOptions) : IRequestHandler<GenerateQiniuUploadTokenQuery, Result>
{
    public async Task<Result> Handle(GenerateQiniuUploadTokenQuery request, CancellationToken cancellationToken)
    {
        var options = authOptions.CurrentValue?.Qiniu ?? throw new Exception("未配置七牛云授权信息");
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


public class GenerateClientQiniuUploadTokenQueryHandler(IOptionsMonitor<AuthorizationSettings> authOptions) : IRequestHandler<GenerateClientQiniuUploadTokenQuery, Result>
{
    public async Task<Result> Handle(GenerateClientQiniuUploadTokenQuery request, CancellationToken cancellationToken)
    {
        var options = authOptions.CurrentValue?.Qiniu ?? throw new Exception("未配置七牛云授权信息");
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
