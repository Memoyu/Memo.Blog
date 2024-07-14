
using JiebaNet.Segmenter;
using Memo.Blog.Application.Common.Text;

namespace Memo.Blog.Infrastructure.Text;

public class SegmenterService : ISegmenterService
{
    private readonly JiebaSegmenter _jiebaSegmenter;
    public SegmenterService(JiebaSegmenter jiebaSegmenter)
    {
        _jiebaSegmenter = jiebaSegmenter;
    }

    public IEnumerable<string> Cut(string text, bool cutAll = false, bool hmm = true)
    {
        return _jiebaSegmenter.Cut(text, cutAll, hmm);
    }

    public string CutWithSplit(string text, string split = " ", bool cutAll = false, bool hmm = true)
    {
        var segs = _jiebaSegmenter.Cut(text, cutAll, hmm);

        return string.Join(split, segs);
    }

    public IEnumerable<string> CutForSearch(string text, bool hmm = true)
    {
        return _jiebaSegmenter.CutForSearch(text, hmm);
    }

    public string CutWithSplitForSearch(string text, string split = " ", bool hmm = true)
    {
        var segs = _jiebaSegmenter.CutForSearch(text, hmm);

        return string.Join(split, segs);
    }
}
