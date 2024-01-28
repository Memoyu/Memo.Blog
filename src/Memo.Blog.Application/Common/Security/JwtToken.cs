namespace Memo.Blog.Application.Common.Security;

public class JwtToken
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
