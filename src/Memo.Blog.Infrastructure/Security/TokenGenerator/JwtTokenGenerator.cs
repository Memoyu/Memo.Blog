using System.IdentityModel.Tokens.Jwt;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Application.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Memo.Blog.Infrastructure.Security.GenerateToken;

public class JwtTokenGenerator(IOptionsMonitor<AuthorizationSettings> authOptions) : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions = authOptions.CurrentValue?.Jwt ?? throw new Exception("未配置服务jwt授权信息");

    public JwtTokenDto GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
        };

        var expiredAt = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOptions.ExpiryInMin));
        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiredAt,
            signingCredentials: signingCredentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        string refreshToken = GenerateRefreshToken();

        var expiredAtTs = expiredAt - new DateTime(1970, 1, 1, 0, 0, 0, 0);

        return new JwtTokenDto ( accessToken, refreshToken, Convert.ToInt64(expiredAtTs.TotalMilliseconds));
    }

    public JwtTokenDto RefreshToken(User user)
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
