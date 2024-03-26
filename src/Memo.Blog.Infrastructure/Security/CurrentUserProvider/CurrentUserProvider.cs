using System.IdentityModel.Tokens.Jwt;
using Memo.Blog.Application.Security;
using Microsoft.AspNetCore.Http;

namespace Memo.Blog.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext is null) return new CurrentUser(0, string.Empty, string.Empty); 
       
        var id = long.Parse(GetSingleClaimValue(ClaimTypes.NameIdentifier) ?? "0");
        var username = GetSingleClaimValue(JwtRegisteredClaimNames.Name) ?? string.Empty;
        var email = GetSingleClaimValue(ClaimTypes.Email) ?? string.Empty;

        return new CurrentUser(id, username, email);
    }

    public string GetClientIp()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context is null) return string.Empty;

        var ip = context.Request.Headers["X-Forwarded-For"].ToString();
        if (string.IsNullOrEmpty(ip))
        {
            if (context.Connection.RemoteIpAddress != null) ip = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        return ip;
    }
    private string? GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User?.Claims?.FirstOrDefault(claim => claim.Type == claimType)?.Value;
}
