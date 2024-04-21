namespace Memo.Blog.Application.OpenSources.Common;

public class OpenSourceClientResult
{
    /// <summary>
    /// 项目Id
    /// </summary>
    public long ProjectId { get; set; }

    /// <summary>
    /// 项目标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///  项目图片
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    ///  项目Readme文档源地址
    /// </summary>
    public string ReadmeUrl { get; set; } = string.Empty;

    /// <summary>
    ///  项目源地址
    /// </summary>
    public string HtmlUrl { get; set; } = string.Empty;

    /// <summary>
    /// Star次数
    /// </summary>
    public int Star { get; set; }

    /// <summary>
    /// Fork次数
    /// </summary>
    public int Fork { get; set; }
}
