using Memo.Blog.Application.Common.Request;

namespace Memo.Blog.Application.Common.Interfaces.Security;

public interface IAuthorizationService
{
    Result AuthorizeCurrentUser<T>(IAuthorizeableRequest<T> request, List<string> requiredRoles, List<string> requiredPermissions, List<string> requiredPolicies);
}
