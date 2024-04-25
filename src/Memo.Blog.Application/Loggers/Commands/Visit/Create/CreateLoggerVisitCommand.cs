namespace Memo.Blog.Application.Loggers.Commands.Visit.Create;

public record CreateLoggerVisitCommand : IAuthorizeableRequest<Result>
{
    /// <summary>
    /// 访问者标识Id
    /// </summary>
    public long VisitorId { get; set; }

    /// <summary>
    /// 访问路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 被访问信息Id（文章Id、动态Id等）
    /// </summary>
    public long? VisitedId { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }
}

public class CreateLoggerVisitCommandValidator : AbstractValidator<CreateLoggerVisitCommand>
{
    public CreateLoggerVisitCommandValidator()
    {
        RuleFor(x => x.VisitorId)
           .NotEmpty()
           .WithMessage("访问者标识Id不能为空");
    }
}

