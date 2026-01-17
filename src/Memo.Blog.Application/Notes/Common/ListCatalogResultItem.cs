namespace Memo.Blog.Application.Notes.Common;

public record ListCatalogResultItem
{
    /// <summary>
    /// 类型：catalog： 0， note： 1
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 目录id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 目录下项数量
    /// </summary>
    public int Count { get; set; }
}
