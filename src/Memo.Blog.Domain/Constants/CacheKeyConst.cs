namespace Memo.Blog.Domain.Constants;

public static class CacheKeyConst
{
    /// <summary>
    /// 用户授权刷新token
    /// </summary>
    /// <param name="refreshToken">刷新token</param>
    /// <returns></returns>
    public static string UserRefreshToken(string refreshToken) => $"memo-blog:user:refresh-token:{refreshToken}";

    /// <summary>
    /// 文章浏览记录缓存key
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="visitorId"></param>
    /// <returns></returns>
    public static string ArticleView(long articleId, long visitorId) => $"memo-blog:article:views:{articleId}:{visitorId}";

    /// <summary>
    /// 友链访问记录缓存key
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="visitorId"></param>
    /// <returns></returns>
    public static string FriendView(long friendId, long visitorId) => $"memo-blog:friend:views:{friendId}:{visitorId}";
}
