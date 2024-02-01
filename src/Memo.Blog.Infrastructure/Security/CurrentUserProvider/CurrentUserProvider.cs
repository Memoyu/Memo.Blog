using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Memo.Blog.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        _httpContextAccessor.HttpContext.ThrowIfNull();

        var id = long.Parse(GetSingleClaimValue(ClaimTypes.NameIdentifier) ?? "0");
        var username = GetSingleClaimValue(JwtRegisteredClaimNames.Name) ?? string.Empty;
        var email = GetSingleClaimValue(ClaimTypes.Email) ?? string.Empty;

        return new CurrentUser(id, username, email);
    }

    private string? GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User?.Claims?.FirstOrDefault(claim => claim.Type == claimType)?.Value;
}
