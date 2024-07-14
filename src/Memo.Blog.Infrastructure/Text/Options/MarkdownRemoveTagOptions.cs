namespace Memo.Blog.Infrastructure.Text.Options;

public class MarkdownRemoveTagOptions
{
    // 移除换行
    public bool RemoveLineBreak { get; set; } = true;

    public bool Gfm { get; set; } = true;

    public bool StripListLeaders { get; set; } = true;

    public bool ListUnicodeChar { get; set; } = false;

    public bool UseImgAltText { get; set; } = false;

    public bool Abbr { get; set; } = false;

    public bool ReplaceLinksWithURL { get; set; } = false;

    public List<string> HtmlTagsToSkip { get; set; } = [];

}
