namespace Memo.Blog.Domain.Enums;
public enum ResultCode
{
    /// <summary>
    /// 失败
    /// </summary>
    Failure = 0,

    /// <summary>
    /// 成功
    /// </summary>
    Success = 1,

    /// <summary>
    /// 令牌过期
    /// </summary>
    TokenExpired = 4010,

    /// <summary>
    /// 令牌过期
    /// </summary>
    TokenInvalidation = 4010,

    /// <summary>
    /// 认证失败
    /// </summary>
    AuthenticationFailure = 4010,
}
