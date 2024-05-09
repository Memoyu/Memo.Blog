namespace Memo.Blog.Application.Loggers.Common;

public class LoggerVisitedResult
{
    /// <summary>
    /// 受访问Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 受访问标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 受访问链接
    /// </summary>
    public string Link { get; set; } = string.Empty;
}
