namespace Memo.Blog.Application.Anlyanis.Common;

/// <summary>
/// 指标数据
/// </summary>
public record MetricItemResult
{
    public MetricItemResult(string name, long value = 0)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// 日期
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 总数
    /// </summary>
    public long Value { get; set; }
};
