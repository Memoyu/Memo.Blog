namespace Memo.Blog.Infrastructure.Security.CurrentUserProvider;

public record CurrentUser(
    long Id,
    string Username,
    string Email);
