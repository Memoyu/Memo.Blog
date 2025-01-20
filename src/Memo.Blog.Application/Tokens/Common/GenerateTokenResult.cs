namespace Memo.Blog.Application.Tokens.Common;

public record GenerateTokenResult(
    long UserId,
    string Username,
    string AccessToken,
    string RefreshToken,
    long ExpiredAt
);
