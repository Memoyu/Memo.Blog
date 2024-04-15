using Memo.Blog.Application.Anlyanis.Common;

namespace Memo.Blog.Application.Articles.Common;

public record SummaryArticleResult
{
    public long Articles { get; set; }

    public long Views { get; set; }

    public long Comments { get; set; }

    public List<MetricItemResult> WeekArticles { get; set; } = [];

    public List<MetricItemResult> WeekViews { get; set; } = [];

    public List<MetricItemResult> WeekComments { get; set; } = [];
}
