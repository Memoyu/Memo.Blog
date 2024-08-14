namespace Memo.Blog.Application.Configs.Common
{
    public record ConfigClientResult
    {
        public BannerConfigResult Banner { get; set; } = new();

        public ColorConfigResult Color { get; set; } = new();
    }
}
