namespace Memo.Blog.Application.Visitors.Common;

public class VisitorWithDetailRegionResult : VisitorResult
{
    /// <summary>
    /// 访问者IP所属国家
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属区域
    /// </summary>
    public string Area { get; set; } = string.Empty;

    /// <summary>
    ///  访问者IP所属省市
    /// </summary>
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属城市
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 访问者IP所属互联网服务商
    /// </summary>
    public string Isp { get; set; } = string.Empty;
}
