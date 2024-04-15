namespace Memo.Blog.Domain.Constants;

public class InitConst
{
    #region 文章分类

    /// <summary>
    /// 未分类CategoryId
    /// </summary>
    public const long InitCategoryId = 1;

    public const string InitCategoryName = "未分类";

    #endregion

    #region 用户

    /// <summary>
    /// 管理员UserId
    /// </summary>
    public const long InitAdminUserId = 1;

    /// <summary>
    /// 管理角色RoleId
    /// </summary>
    public const long InitAdminRoleId = 1;

    /// <summary>
    /// 游客UserId
    /// </summary>
    public const long InitVisitorUserId = 2;

    /// <summary>
    /// 游客角色RoleId
    /// </summary>
    public const long InitVisitorRoleId = 2;

    #endregion
}
