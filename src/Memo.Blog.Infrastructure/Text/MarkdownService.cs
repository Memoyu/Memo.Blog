using System.Text.RegularExpressions;
using Memo.Blog.Application.Common.Text;
using Memo.Blog.Infrastructure.Text.Options;

namespace Memo.Blog.Infrastructure.Text;

/// <summary>
/// Markdown处理服务
/// 实现来源：https://github.com/zuchka/remove-markdown 
/// </summary>
public class MarkdownService : IMarkdownService
{
    private readonly MarkdownRemoveTagOptions _options;

    public MarkdownService()
    {
        _options = new MarkdownRemoveTagOptions();
    }

    public string RemoveTag(string text)
    {
        string output = text;

        // Remove horizontal rules (stripListHeaders conflict with this rule, which is why it has been moved to the top)
        output = Regex.Replace(output, @"^(-\s*?|\*\s*?|_\s*?){3,}\s*", "", RegexOptions.Multiline);

        if (_options.StripListLeaders)
        {
            if (_options.ListUnicodeChar)
                output = Regex.Replace(output, @"^([\s\t]*)([\*\-\+]|\d+\.)\s+", _options.ListUnicodeChar + " $1", RegexOptions.Multiline);
            else
                output = Regex.Replace(output, @"^([\s\t]*)([\*\-\+]|\d+\.)\s+", "$1", RegexOptions.Multiline);
        }

        if (_options.Gfm)
        {
            // Header
            output = Regex.Replace(output, @"\n={2,}", "\n");
            // Fenced codeblocks
            output = Regex.Replace(output, @"~{3}.*\n", "\n");
            // Strikethrough
            output = Regex.Replace(output, @"~~", string.Empty);
            // Fenced codeblocks
            output = Regex.Replace(output, @"`{3}.*\n", string.Empty);
        }

        if (_options.Abbr)
        {
            // Remove abbreviations
            output = Regex.Replace(output, @"\*\[.*\]:.*\n", string.Empty);
        }

        // Remove HTML tags
        output = Regex.Replace(output, @"<[^>]*>", string.Empty);

        var htmlReplaceRegex = @"<[^>]*>";
        var ro = RegexOptions.None;
        if (_options.HtmlTagsToSkip.Count > 0)
        {
            // Using negative lookahead. Eg. (?!sup|sub) will not match 'sup' and 'sub' tags.
            var joinedHtmlTagsToSkip = @"(?!" + string.Join("|", _options.HtmlTagsToSkip) + ")";

            // Adding the lookahead literal with the default regex for html. Eg./<(?!sup|sub)[^>]*>/ig
            htmlReplaceRegex = @"<" + joinedHtmlTagsToSkip + @"[^>]*>";
            ro = RegexOptions.IgnoreCase;
        }

        // Remove HTML tags
        output = Regex.Replace(output, htmlReplaceRegex, string.Empty, ro);
        // Remove setext-style headers
        output = Regex.Replace(output, @"^[=\-]{2,}\s*$", string.Empty);
        // Remove footnotes?
        output = Regex.Replace(output, @"\[\^.+?\](\: .*?$)?", string.Empty);
        output = Regex.Replace(output, @"\s{0,2}\[.*?\]: .*?$", string.Empty);

        // Remove images
        output = Regex.Replace(output, @"\!\[(.*?)\][\[\(].*?[\]\)]", _options.UseImgAltText ? "$1" : string.Empty);
        // Remove inline links
        output = Regex.Replace(output, @"\[([^\]]*?)\][\[\(].*?[\]\)]", _options.ReplaceLinksWithURL ? "$2" : "$1");
        // Remove blockquotes
        output = Regex.Replace(output, @"^(\n)?\s{0,3}>\s?", "$1", RegexOptions.Multiline);
        // output = Regex.Replace(output, @"(^|\n)\s{0,3}>\s?", '\n\n')
        // Remove reference-style links?
        output = Regex.Replace(output, @"^\s{1,2}\[(.*?)\]: (\S+)( "".*?"")?\s*$", string.Empty);
        // Remove atx-style headers
        output = Regex.Replace(output, @"^(\n)?\s{0,}#{1,6}\s*( (.+))? +#+$|^(\n)?\s{0,}#{1,6}\s*( (.+))?$", "$1$3$4$6", RegexOptions.Multiline);
        // Remove * emphasis
        output = Regex.Replace(output, @"([\*]+)(\S)(.*?\S)??\1", "$2$3");
        // Remove _ emphasis. Unlike *, _ emphasis gets rendered only if 
        //   1. Either there is a whitespace character before opening _ and after closing _.
        //   2. Or _ is at the start/end of the string.
        output = Regex.Replace(output, @"(^|\W)([_]+)(\S)(.*?\S)??\2($|\W)", "$1$3$4$5");
        // Remove code blocks
        output = Regex.Replace(output, @"(`{3,})(.*?)\1", "$2", RegexOptions.Multiline);
        // Remove inline code
        output = Regex.Replace(output, @"`(.+?)`", "$1");
        // // Replace two or more newlines with exactly two? Not entirely sure this belongs here...
        // output = Regex.Replace(output, @"\n{2,}", '\n\n')
        // // Remove newlines in a paragraph
        // output = Regex.Replace(output, @"(\S+)\n\s*(\S+)", '$1 $2')
        // Replace strike through
        output = Regex.Replace(output, @"~(.*?)~", "$1");

        // 移除转义字符/分隔字符
        output = Regex.Replace(output, @"&+[A-Za-z0-9#]+;", string.Empty);

        if (_options.RemoveLineBreak)
        {
            output = output.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        return output;
    }
}
