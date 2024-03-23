using System.Net;

namespace Memo.Blog.Application.Common.Interfaces.Region;

public interface IRegionSearcher
{
    RegionInfo Search(string ipStr);

    RegionInfo Search(IPAddress ipAddress);

    RegionInfo Search(uint ipAddress);
}
