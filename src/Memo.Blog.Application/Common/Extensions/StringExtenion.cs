using System.Text;

namespace Memo.Blog.Application.Common.Extensions;

public static class StringExtenion
{
    private static readonly Encoding _utf8Encoder = Encoding.GetEncoding( "UTF-8", new EncoderReplacementFallback(string.Empty),  new DecoderExceptionFallback() );

    public static string ToUtf8(this string value)
    {
        return _utf8Encoder.GetString(_utf8Encoder.GetBytes(value));
    }
}
