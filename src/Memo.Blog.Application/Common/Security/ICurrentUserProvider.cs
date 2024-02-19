namespace Memo.Blog.Application.Security;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}
