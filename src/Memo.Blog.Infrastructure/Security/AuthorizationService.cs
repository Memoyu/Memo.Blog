using Memo.Blog.Application.Common.Request;

namespace Memo.Blog.Infrastructure.Security;

public class AuthorizationService(ICurrentUserProvider _currentUserProvider) : IAuthorizationService
{
    public Result AuthorizeCurrentUser<T>(IAuthorizeableRequest<T> request, List<string> requiredRoles, List<string> requiredPermissions, List<string> requiredPolicies)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        // TODO：完善鉴权逻辑
        //if (requiredPermissions.Except(currentUser.Permissions).Any())
        //{
        //    return Error.Unauthorized(description: "User is missing required permissions for taking this action");
        //}

        //if (requiredRoles.Except(currentUser.Roles).Any())
        //{
        //    return Error.Unauthorized(description: "User is missing required roles for taking this action");
        //}

        //foreach (var policy in requiredPolicies)
        //{
        //    var authorizationAgainstPolicyResult = _policyEnforcer.Authorize(request, currentUser, policy);

        //    if (authorizationAgainstPolicyResult.IsError)
        //    {
        //        return authorizationAgainstPolicyResult.Errors;
        //    }
        //}

        return Result.Success();
    }
}
