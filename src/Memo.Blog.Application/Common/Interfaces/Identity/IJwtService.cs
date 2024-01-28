using Memo.Blog.Application.Common.Security;
using Memo.Blog.Domain.Entities;

namespace Memo.Blog.Application.Common.Interfaces.Identity;

public interface IJwtService
{
    /// <summary>
    /// 生成JWT Token
    /// </summary>
    /// <param name="user">用户信息</param>
    /// <returns></returns>
    JwtToken GenerateToken(User user);

    /// <summary>
    /// 刷新JWT Token
    /// </summary>
    /// <param name="user">用户信息</param>
    /// <returns></returns>
    JwtToken RefreshToken(User user);
}
