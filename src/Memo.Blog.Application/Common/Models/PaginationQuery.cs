namespace Memo.Blog.Application.Common.Models;

public record PaginationQuery
{
    /// <summary>
    /// 每页条数
    /// </summary>
    public int Size { get; set; } = 15;

    /// <summary>
    /// 从0开始，0时取第1页，1时取第二页
    /// </summary>
    public int Page { get; set; } = 1;

    public string Sort { get; set; } = string.Empty;
}
