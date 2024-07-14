namespace Memo.Blog.Application.Common.Text;
public interface IMarkdownService
{
    /// <summary>
    /// 移除Markdown关键字及标签，保留纯文字
    /// </summary>
    /// <param name="text">Markdown文本</param>
    /// <returns></returns>
    public string RemoveTag(string text);
}
