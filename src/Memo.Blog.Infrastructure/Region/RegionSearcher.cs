using System.Net;
using IP2Region.Net.Abstractions;
using Memo.Blog.Application.Common.Interfaces.Region;

namespace Memo.Blog.Infrastructure.Region;

public class RegionSearcher(ISearcher searcher) : IRegionSearcher
{
    public RegionInfo Search(string ipStr)
    {
        var region = searcher.Search(ipStr);
        return GetRegionDto(region);
    }

    public RegionInfo Search(IPAddress ipAddress)
    {
        var region = searcher.Search(ipAddress);
        return GetRegionDto(region);
    }

    public RegionInfo Search(uint ipAddress)
    {
        var region = searcher.Search(ipAddress);
        return GetRegionDto(region);
    }

    private RegionInfo GetRegionDto(string? region)
    {
        if (string.IsNullOrWhiteSpace(region)) return new();
        var regions = region.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)?.ToList() ?? [];
        if (regions.Count < 5) return new();
        return new RegionInfo
        {
            Country = regions[0] == "0" ? string.Empty : regions[0],
            Region = regions[1] == "0" ? string.Empty : regions[1],
            Province = regions[2] == "0" ? string.Empty : regions[2],
            City = regions[3] == "0" ? string.Empty : regions[3],
            Isp = regions[4] == "0" ? string.Empty : regions[4],
        };
    }
}
