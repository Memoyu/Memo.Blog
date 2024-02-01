namespace Memo.Blog.Infrastructure.Security.GenerateToken;

public class JwtOptions
{
    public const string Section = "JwtOptions";

    /// <summary>
    /// 密钥
    /// </summary>
    public required string Secret { get; set; }

    /// <summary>
    /// 签发人
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// 受众
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// Jwt Token 过期时间（分钟） 
    /// </summary>
    public int ExpiryInMin { get; set; } = 120;
}
