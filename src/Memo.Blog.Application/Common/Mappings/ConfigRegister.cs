using Memo.Blog.Application.Configs.Common;

namespace Memo.Blog.Application.Common.Mappings;

public class ConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Config, ConfigClientResult>()
            .Map(d => d.Banner, s => GetBannerConfig(s.Banner))
            .Map(d => d.Color, s => GetStyleConfig(s.Color));

        config.ForType<Config, ConfigResult>()
            .Map(d => d.BannerJson, s => s.Banner)
            .Map(d => d.ColorJson, s => s.Color)
            .Map(d => d.Banner, s => GetBannerConfig(s.Banner))
            .Map(d => d.Color, s => GetStyleConfig(s.Color));
    }

    private BannerConfigResult GetBannerConfig(string banner) => string.IsNullOrWhiteSpace(banner) ? new() : banner.ToDesJson<BannerConfigResult>() ?? new();

    private ColorConfigResult GetStyleConfig(string color) => string.IsNullOrWhiteSpace(color) ? new() : color.ToDesJson<ColorConfigResult>() ?? new();
}
