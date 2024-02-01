namespace Memo.Blog.Application.Common.Security;

public record GenerateTokenResult(
    long UserId,
    string Username,
    string AccessToken,
    string RefreshToken
);
