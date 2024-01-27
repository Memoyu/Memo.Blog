namespace Memo.Blog.Infrastructure.Identity;

public class JwtOptions
{
    /// <summary>
    /// 密钥
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// 签发人
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 受众
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Jwt Token 过期时间（分钟） 
    /// </summary>
    public int ExpiryInMin { get; set; }
}