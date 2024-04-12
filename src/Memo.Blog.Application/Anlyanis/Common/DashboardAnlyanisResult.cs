namespace Memo.Blog.Application.Anlyanis.Common;
public record DashboardAnlyanisResult
{
    /// <summary>
    /// 汇总数据
    /// </summary>
    public SummaryAnlyanisResult Summary { get; set; } = new();

    /// <summary>
    /// UV分析数据
    /// </summary>
    public UniqueVisitorAnlyanisResult UniqueVisitor { get; set; } = new();

    /// <summary>
    /// PV分析数据
    /// </summary>
    public PageVisitorAnlyanisResult PageVisitor { get; set; } = new();

    /// <summary>
    /// 评论分析数据
    /// </summary>
    public CommentAnlyanisResult Comment { get; set; } = new();
}
