using Memo.Blog.Application.Common.Interfaces.Identity;
using Memo.Blog.Application.Common.Security;
using Memo.Blog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Memo.Blog.Infrastructure.Identity;
public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtToken GenerateToken(User user)
    {
        var jwtOptions = _configuration.GetValue<JwtOptions>("JwtOptions") ?? throw new ArgumentNullException("JwtOptions is not null");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Nickname),
            new Claim(JwtRegisteredClaimNames.Jti, user.Username),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtOptions.ExpiryInMin)),
            signingCredentials: signingCredentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        string refreshToken = GenerateRefreshToken();

        return new JwtToken { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public JwtToken RefreshToken(User user)
    {
        if (user is null)
            throw new ArgumentException("用户信息不能为空");

        if (DateTime.Compare(user.LastLoginTime, DateTime.Now) > new TimeSpan(5, 0, 0, 0).Ticks)
            throw new InvalidOperationException("请重新登录");

        var jwtToken = GenerateToken(user);

        return jwtToken;
    }

    /// <summary>
    /// 生成RefreshToken
    /// </summary>
    /// <param name="size">长度</param>
    /// <returns></returns>
    private string GenerateRefreshToken(int size = 32)
    {
        var randomNumber = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
