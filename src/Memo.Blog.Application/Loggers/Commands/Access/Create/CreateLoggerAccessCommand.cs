﻿using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Loggers.Commands.Access.Create;

[Authorize(Permissions = ApiPermission.LoggerAccess.Create)]
public record CreateLoggerAccessCommand : IAuthorizeableRequest<Result>
{
    /// <summary>
    /// 访问者标识Id
    /// </summary>
    public string VisitorId { get; set; } = string.Empty;

    /// <summary>
    /// 访问路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 访问行为
    /// </summary>
    public AccessLogBehavior Behavior { get; set; }

    /// <summary>
    /// 被访问信息Id（文章Id、动态Id等）
    /// </summary>
    public long VisitedId { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }
}

public class CreateLoggerAccessCommandValidator : AbstractValidator<CreateLoggerAccessCommand>
{
    public CreateLoggerAccessCommandValidator()
    {
        RuleFor(x => x.VisitorId)
           .NotEmpty()
           .WithMessage("访问者标识Id不能为空");

        RuleFor(x => x.VisitedId)
           .Must(x => x > 0)
           .WithMessage("被访问信息Id不能小于0");

        RuleFor(x => x.Behavior)
            .IsInEnum()
            .WithMessage("访问行为错误");
    }
}
