namespace Memo.Blog.Application.Security;

public record JwtTokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiredAt
);
