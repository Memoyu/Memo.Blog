﻿namespace Memo.Blog.Application.FileStorages.Queries.Generate;

[Authorize(Permissions = ApiPermission.FileStorage.GenerateQiniuUploadToken)]
public record GenerateQiniuUploadTokenQuery(string Path) : IAuthorizeableRequest<Result>;

public class GenerateQiniuUploadTokenQueryValidator : AbstractValidator<GenerateQiniuUploadTokenQuery>
{
    public GenerateQiniuUploadTokenQueryValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty()
            .WithMessage("上传路径不能为空");
    }
}

public record GenerateClientQiniuUploadTokenQuery(string Path) : IRequest<Result>;

public class GenerateClientQiniuUploadTokenQueryValidator : AbstractValidator<GenerateClientQiniuUploadTokenQuery>
{
    public GenerateClientQiniuUploadTokenQueryValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty()
            .WithMessage("上传路径不能为空");
    }
}
