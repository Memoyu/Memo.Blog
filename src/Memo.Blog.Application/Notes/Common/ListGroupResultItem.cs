namespace Memo.Blog.Application.Notes.Common;

public record ListGroupResultItem
{
    /// <summary>
    /// 类型：group： 0， note： 1
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 分组id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 分组下项数量
    /// </summary>
    public int Count { get; set; }
}
