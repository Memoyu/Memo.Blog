using System.Security.Claims;
using Memo.Blog.Application.Common.Interfaces;

namespace Memo.Blog.WebApi.Services;
public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal? _currentPrincipal;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _currentPrincipal = httpContextAccessor.HttpContext?.User;
    }

    public long? Id => GetClaimsPrincipalValue<long>();

    private T? GetClaimsPrincipalValue<T>()
    {
        Claim? claimOrNull = _currentPrincipal?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return claimOrNull == null || string.IsNullOrWhiteSpace(claimOrNull.Value) ? default : (T)Convert.ChangeType(claimOrNull.Value, typeof(T));
    }
}
