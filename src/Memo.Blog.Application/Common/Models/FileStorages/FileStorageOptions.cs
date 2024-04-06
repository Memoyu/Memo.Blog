namespace Memo.Blog.Application.Common.Models.FileStorages;

public record QiniuOptions
{
    public const string Section = "Qiniu";

    public string AK { get; set; } = string.Empty;

    public string SK { get; set; } = string.Empty;

    public string Bucket { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public bool UseHttps { get; set; }
}
