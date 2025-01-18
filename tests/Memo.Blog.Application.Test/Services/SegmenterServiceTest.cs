using JiebaNet.Segmenter;
using Memo.Blog.Application.Common.Text;
using Memo.Blog.Infrastructure.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Memo.Blog.Application.Test.Services;

public class SegmenterServiceTest
{
    private readonly JiebaSegmenter _jiebaSegmenter;
    private readonly ISegmenterService _segmenterService;

    public SegmenterServiceTest()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new JiebaSegmenter());
        services.AddScoped<ISegmenterService, SegmenterService>();
        var sp = services.BuildServiceProvider();

        _jiebaSegmenter = sp.GetRequiredService<JiebaSegmenter>();
        _segmenterService = sp.GetRequiredService<ISegmenterService>();
    }

    [Fact]
    public async Task CutForSearch_Should_Success()
    {
        var words = _jiebaSegmenter.CutForSearch("小明硕士毕业于中国科学院计算所，后在日本京都大学深造");
        Assert.NotEmpty(words);
        Assert.Equal("小明", words.FirstOrDefault());
    }

    [Fact]
    public async Task CutWithSplitForSearch_Should_Success()
    {
        var words = _segmenterService.CutWithSplitForSearch("小明硕士毕业于中国科学院计算所，后在日本京都大学深造");
        Assert.NotEmpty(words);
    }
}
