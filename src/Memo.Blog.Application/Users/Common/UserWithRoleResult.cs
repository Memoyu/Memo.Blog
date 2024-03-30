using Memo.Blog.Application.Roles.Common;

namespace Memo.Blog.Application.Users.Common;

public class UserWithRoleResult : UserResult
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public List<RoleListResult> Roles { get; set; } = [];
}
