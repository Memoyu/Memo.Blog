using Memo.Blog.Application.Common.Interfaces.Region;
using Memo.Blog.Application.Visitors.Common;

namespace Memo.Blog.Application.Common.Mappings;

public class VisitorRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Visitor, VisitorResult>()
            .Map(d => d.Region, s => GetRegionFormat(s.Region));

        config.ForType<Visitor, VisitorWithDetailRegionResult>()
            .Map(d => d.Country, s => GetRegionByIndex(s.Region, 0))
            .Map(d => d.Area, s => GetRegionByIndex(s.Region, 1))
            .Map(d => d.Province, s => GetRegionByIndex(s.Region, 2))
            .Map(d => d.City, s => GetRegionByIndex(s.Region, 3))
            .Map(d => d.Isp, s => GetRegionByIndex(s.Region, 4))
            .Map(d => d.Region, s => GetRegionFormat(s.Region));
    }

    private string GetRegionFormat(string region)
    {
        var searcher = MapContext.Current.GetService<IRegionSearcher>() ?? throw new Exception("未注册IRegionSearcher服务");
        var regionIfon = searcher.ToRegionInfo(region);
        return regionIfon.GetRegion() ?? string.Empty;
    }

    private string GetRegionByIndex(string region, int index)
    {
        var regs = region.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        return regs.Length <= index ? string.Empty : regs[index];
    }
}
