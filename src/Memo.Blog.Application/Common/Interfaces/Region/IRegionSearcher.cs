using System.Net;

namespace Memo.Blog.Application.Common.Interfaces.Region;

public interface IRegionSearcher
{
    string Search(string ipStr);

    string Search(IPAddress ipAddress);

    string Search(uint ipAddress);

    RegionInfo SearchInfo(string ipStr);

    RegionInfo SearchInfo(IPAddress ipAddress);

    RegionInfo SearchInfo(uint ipAddress);

    RegionInfo ToRegionInfo(string? region);
}
