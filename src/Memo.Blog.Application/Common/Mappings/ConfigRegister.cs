using Memo.Blog.Application.Configs.Commands.Update;
using Memo.Blog.Application.Configs.Common;

namespace Memo.Blog.Application.Common.Mappings;

public class ConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UpdateConfigCommand, Config>()
           .Map(d => d.Banner, s => ToJson(s.Banner))
           .Map(d => d.Color, s => ToJson(s.Color));

        config.ForType<Config, ConfigClientResult>()
            .Map(d => d.Banner, s => GetBannerConfig(s.Banner))
            .Map(d => d.Color, s => GetStyleConfig(s.Color));

        config.ForType<Config, ConfigResult>()
            .Map(d => d.BannerJson, s => s.Banner)
            .Map(d => d.ColorJson, s => s.Color)
            .Map(d => d.Banner, s => GetBannerConfig(s.Banner))
            .Map(d => d.Color, s => GetStyleConfig(s.Color));
    }

    private string ToJson(object obj) => obj.ToJson();

    private BannerConfig GetBannerConfig(string banner) => string.IsNullOrWhiteSpace(banner) ? new() : banner.ToDesJson<BannerConfig>() ?? new();

    private ColorConfig GetStyleConfig(string color) => string.IsNullOrWhiteSpace(color) ? new() : color.ToDesJson<ColorConfig>() ?? new();
}
